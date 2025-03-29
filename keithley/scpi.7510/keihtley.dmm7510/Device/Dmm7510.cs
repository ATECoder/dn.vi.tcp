using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Keithley.Tcp.Client;

namespace Keithley.Dmm7510.Device;

public partial class DMM7510 : IDisposable
{
    private readonly TcpSession _tcpSession;

    public DMM7510(string ipv4Address, int sampleRate, int measurementFunction,
        Single measurementRange, int bufferSize)
    {
        this._tcpSession = new TcpSession( ipv4Address );
        this.SampleRate = sampleRate;
        this.MeasurementFunction = measurementFunction == 0 ? "VOLT" : "CURR";
        this.MeasurementRange = measurementRange;
        this.BufferSize = bufferSize;
    }

    public void Dispose()
    {
        this.Dispose( true );
    }

    private void Dispose( bool disposing )
    {
        if ( disposing )
        {
            this._tcpSession?.Dispose();
        }
    }

    public void Connect( bool echoIdentity, ref string identity )
    {
        Console.WriteLine( "Connected to instrument......" );
        this._tcpSession.Connect(echoIdentity, "*IDN?", ref identity, true);
        this._tcpSession.ReceiveTimeout = TimeSpan.FromMilliseconds( 20000 );
        this._tcpSession.ReceiveBufferSize = 35565;
    }

    public void Disconnect()
    {
        this._tcpSession.Disconnect();
    }

    public int SampleRate { get; private set; }
    public string MeasurementFunction { get; private set; }
    public Single MeasurementRange { get; private set; }

    public int BufferSize { get; private set; }

    public bool IsBufferRollover { get; private set; }

    public void Setup_Buffers()
    {
        _ = this._tcpSession.WriteLine( "*RST" );
        _ = this._tcpSession.WriteLine( "TRAC:CLE \"defbuffer1\"" );
        _ = this._tcpSession.WriteLine( "TRAC:POIN " + Convert.ToString( this.BufferSize ) + ", \"defbuffer1\"" );
        _ = this._tcpSession.WriteLine( "TRAC:FILL:MODE CONT, \"defbuffer1\"" );
        _ = this._tcpSession.WriteLine( "*WAI" );
        Thread.Sleep(100);
    }

    public void Setup_DMM()
    {
        // Do setup...
        _ = this._tcpSession.WriteLine( ":SENS:DIG:FUNC \"" + this.MeasurementFunction + "\"" );
        _ = this._tcpSession.WriteLine( ":DIG:" + this.MeasurementFunction + ":RANG " + Convert.ToString( this.MeasurementRange ) );
        _ = this._tcpSession.WriteLine( ":SENS:DIG:" + this.MeasurementFunction + ":SRAT " + Convert.ToString( this.SampleRate ) );
        _ = this._tcpSession.WriteLine( ":SENS:DIG:COUNt " + Convert.ToString( this.BufferSize ) );
        _ = this._tcpSession.WriteLine( ":SENS:DIG:" + this.MeasurementFunction + ":APER 5e-6" );        // was 1e-6
        _ = this._tcpSession.WriteLine( ":FORM:DATA SRE" );                                 // for single precision
    }

    public void Setup_Digitizing(int captureMinutes)
    {
        _ = this._tcpSession.WriteLine( ":TRIGger:LOAD \"EMPTY\"" );
        _ = this._tcpSession.WriteLine( ":TRIGger:BLOCk:DIGitize 1, \"defbuffer1\", " + Convert.ToString( this.BufferSize ) );
        _ = this._tcpSession.WriteLine( ":TRIGger:BLOCk:BRANch:COUNter 2, " + Convert.ToString( captureMinutes * 2 ) + ", 1" );
    }

    public void Trigger_DMM()
    {
        // Do Trigger...
        _ = this._tcpSession.WriteLine( "INIT" );
    }

    /// <summary>   Extracts the buffer data. </summary>
    /// <remarks>   This needs to be fixed for seconds duration and lower sample rates. </remarks>
    /// <param name="filePath">             full path name of the file. </param>
    /// <param name="unitId">               Identifier for the unit. </param>
    /// <param name="bufferName">           Name of the buffer. </param>
    /// <param name="bufferSize">           Size of the buffer. </param>
    /// <param name="bytesRcvCnt">          Number of bytes receives. </param>
    /// <param name="chunkSize">            Size of the chunk of single precision values. </param>
    /// <param name="stopWatch">            [in,out] The stop watch. </param>
    /// <param name="duration">             The duration in seconds. </param>
    /// <param name="savedReadingsCount">   [in,out] Number of saved readings. </param>
    public void ExtractBufferData(string filePath, string unitId, String bufferName, int bufferSize,
                                  int chunkSize, ref Stopwatch stopWatch, int duration, ref int savedReadingsCount)
    {
        int startIndex = 1;
        int endIndex = chunkSize;
        int totalReadings = 0;
        float[] readingAmounts = new float[chunkSize];
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
        string fileName = "DMM7510_StreamDigitized_" + unitId + "_" + timeStamp + ".csv";
        string fullFileName = Path.Combine( filePath, fileName );
        int lastCount = 0 ;
        int previousReadingBufferIndex = 0;
        int k = 0;
        bool doPoll = true;
        this.IsBufferRollover = false;
        string queryReply = "";
        int huh;
        do
        {
            string queryCommand;
            if ( doPoll )
            {
                int cumulativeCount;
                do
                {
                    queryReply = "";
                    // read last buffer index
                    // use END because plain old ACT? tops off at bufferSize when full
                    queryCommand = ":TRACe:ACTual:END? \"" + bufferName + "\"";
                    _ = this._tcpSession.QueryLine( queryCommand, 256, ref queryReply, true );
                    this._tcpSession.Flush();

                    int readingBufferIndex = Convert.ToInt32( queryReply );
                    if ( readingBufferIndex < previousReadingBufferIndex )
                    {
                        k++;                    // this indicates a buffer roll-over condition
                        this.IsBufferRollover = true;
                    }
                    cumulativeCount = k * bufferSize + readingBufferIndex;
                    previousReadingBufferIndex = readingBufferIndex;

                } while ( (cumulativeCount - totalReadings) < chunkSize );
            }

            // Pulling the data back from the instrument means there's a bottleneck with the network...
            if ( endIndex > bufferSize )
                endIndex = bufferSize;

            queryReply = "";
            queryCommand = ":TRAC:DATA? " + Convert.ToString( startIndex ) + ", " + Convert.ToString( endIndex ) + ", \"" + bufferName + "\"";
            // _ = this._tcpSession.QueryLine( queryCommand, 2, bytesRcvCnt, ref readingAmounts );
            _ = this._tcpSession.QueryLine( queryCommand, 2, chunkSize, ref readingAmounts );
            this._tcpSession.Flush();

            // Generate the file name based on the system timestamp...
            int writeCounter = totalReadings / bufferSize;
            if ( writeCounter > lastCount )
            {
                // create a new file name...
                timeStamp = DateTime.Now.ToString( "yyyy-MM-dd_hh-mm-ss" );

                fileName = "DMM7510_StreamDigitized_" + unitId + "_" + timeStamp + ".csv";
                fullFileName = Path.Combine( filePath, fileName );

                lastCount++;
            }

            WriteToFile( fullFileName, readingAmounts );

            totalReadings += chunkSize;
            startIndex = (totalReadings % bufferSize) + 1;
            endIndex = startIndex + (chunkSize - 1);

            // Write to file in dh: this changed to 1 second intervals from 30s intervals
            huh = bufferSize * (duration * 2) - chunkSize;
        } while ( totalReadings < huh ); // (!(rcvBuffer.Contains("IDLE")));

        savedReadingsCount = totalReadings;

        stopWatch.Stop();
    }

    /// <summary>   Writes to file. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="fileName"> Filename of the file. </param>
    /// <param name="values">   The values. </param>
    private static void WriteToFile(String fileName, float[] values)
    {
        bool doAppend = true;
        using StreamWriter writer = new ( fileName, doAppend );
        foreach ( var value in values )
        {
            writer.WriteLine( value );
        }

    }
}
