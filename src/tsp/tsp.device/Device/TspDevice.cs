using System.Runtime.InteropServices;
using cc.isr.Tcp.Client;

namespace cc.isr.Tcp.Tsp.Device;

/// <summary>   A TSP Device. </summary>
/// <remarks>   2024-02-05. </remarks>
public partial class TspDevice : IDisposable
{

    #region " Construction and Cleanup "

    /// <summary>   Constructor. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">          The IPv4 address. </param>
	public TspDevice(string ipv4Address)
	{
        this.Session = new TcpSession( ipv4Address  );
	}

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    /// resources.
    /// </summary>
    /// <remarks>   2024-02-05. </remarks>
    public void Dispose()
    {
        this.Dispose( true );
    }

    private void Dispose( bool disposing )
    {
        if ( disposing )
        {
            this.Session?.Dispose();
        }
    }

    #endregion

    #region " TCP Session "

    /// <summary>   Gets or sets the session. </summary>
    /// <value> The session. </value>
    public TcpSession Session { get; private set; }

    /// <summary>   Connects. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="echoIdentity"> True to echo identity. </param>
    /// <param name="identity">     [in,out] The identity. </param>
    public void Connect( bool echoIdentity, ref string identity )
    {
        Console.WriteLine( "Connected to instrument......" );
        this.Session.Connect(echoIdentity, "*IDN?", ref identity, true);
        this.Session.ReceiveTimeout = TimeSpan.FromMilliseconds( 3000 );
        this.Session.ReceiveBufferSize = 35565;
    }

    /// <summary>   Disconnects this object. </summary>
    /// <remarks>   2024-02-05. </remarks>
    public void Disconnect()
    {
        this.Session.Disconnect();  
    }

    #endregion

    #region " Contants "

    /// <summary>   (Immutable) the DC Voltage source function. </summary>
    public const string DCVoltageSourceFunction = "OUTPUT_DCVOLTS";

    /// <summary>   (Immutable) the DC Current source function. </summary>
    public const string DCCurrentSourceFunction = "OUTPUT_DCAMPS";

    #endregion

    #region " Settings "

    /// <summary>   Gets the aperture in power line cycles. </summary>
    /// <value> The aperture. </value>
    public double Aperture { get; set; } = 1;

    /// <summary>   Gets the current level to source or limit in Amperes. </summary>
    /// <value> The current. </value>
    public double CurrentLevel { get; set; } = 0.01;

    /// <summary>   Gets the voltage to source or limit in volts. </summary>
    /// <value> The voltage. </value>
    public double VoltageLevel { get; set; } = 0.1;

    /// <summary>   Gets the source function, e.g., current or voltage source. </summary>
    /// <value> The source function. </value>
    public string SourceFunction { get; set; } = DCVoltageSourceFunction;

    /// <summary>   Gets or sets the source-measure unit. </summary>
    /// <value> The smu. </value>
    public string SMU { get; set; } = "_G.smua";

    /// <summary>   Gets a value indicating whether the automatic range is used. </summary>
    /// <value> True if automatic range, false if not. </value>
    public bool AutoRange { get; set; } = true;

    #endregion

    #region " Readings " 

    /// <summary>   Gets or sets the current reading. </summary>
    /// <value> The current reading. </value>
    public string CurrentReading { get; private set; } = string.Empty;

    /// <summary>   Gets or sets the voltage reading. </summary>
    /// <value> The voltage reading. </value>
    public string VoltageReading { get; private set; } = string.Empty;


    /// <summary>   Gets or sets the current Value. </summary>
    /// <value> The current Value. </value>
    public double CurrentValue { get; private set; } = default;

    /// <summary>   Gets or sets the voltage Value. </summary>
    /// <value> The voltage Value. </value>
    public double VoltageValue { get; private set; } = default;

    /// <summary>   Gets or sets the calculated resistance. </summary>
    /// <value> The resistance. </value>
    public double? Resistance { get; private set; } = default;

    #endregion

    #region " Query Device " 

    /// <summary>   Queries device information. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="request">   The requested information. </param>
    /// <returns>   The device information. </returns>
    public string QueryDeviceInfo( string request )
    {
        string reply = string.Empty;
        _ = this.Session.QueryLine($"_G.print({request})", 1024, ref reply, true);
        return reply;
    }

    #endregion

    #region " Basic operations " 

    /// <summary>   Resets the known state. </summary>
    /// <remarks>   2024-02-05. </remarks>
    public void ResetKnownState()
    {

        this.ClearReadings();

        // turn off prompts and error messages
        _ = this.Session.WriteLine($"_G.localnode.showerrors=0");
        _ = this.Session.WriteLine($"_G.localnode.prompts=0");

        _ = this.Session.WriteLine("_G.reset() _G.waitcomplete() _G.print([[1]])");
        Thread.Sleep(100);

        string reply = string.Empty;
        _ = this.Session.Read(1024, ref reply, true);

        if ( reply != "1") 
            throw new ExternalException( $"Failed resetting know state; *OPC returned '{reply}' rather than '1'." );

    }

    #endregion

    #region " Configure and Measure Ohm " 

    /// <summary>   Clears the readings. </summary>
    /// <remarks>   2024-02-05. </remarks>
    public void ClearReadings()
    {
        this.CurrentReading = string.Empty;
        this.VoltageReading = string.Empty; 
        this.VoltageValue = default;
        this.CurrentValue = default;
        this.Resistance = default;  
    }

    /// <summary>   Configure Constant Source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    public void ConfigureConstantSource()
    {
        this.ResetKnownState();

        // configure the measurement
        _ = this.Session.WriteLine($"local m = {this.SMU}.measure");

        if (this.AutoRange )
        {
            _ = this.Session.WriteLine($"m.autorangei = {this.SMU}.AUTORANGE_ON");
            _ = this.Session.WriteLine($"m.autorangev = {this.SMU}.AUTORANGE_ON");
        }
        else
        {
            _ = this.Session.WriteLine($"m.autorangei = {this.SMU}.AUTORANGE_OFF");
            _ = this.Session.WriteLine($"m.rangei = {2 * this.CurrentLevel}");
            _ = this.Session.WriteLine($"m.rangev = {2 * this.VoltageLevel}");
        }
        _ = this.Session.WriteLine($"m.nplc= {this.Aperture}");
        _ = this.Session.WriteLine($"m.autozero= {this.SMU}.AUTOZERO_AUTO");
        _ = this.Session.WriteLine($"m.count = 1");

        // configure the source
        _ = this.Session.WriteLine($"local s = {this.SMU}.source");

        if ( this.SourceFunction == TspDevice.DCVoltageSourceFunction)
        {
            _ = this.Session.WriteLine($"s.func = {this.SMU}.{TspDevice.DCVoltageSourceFunction}");
            _ = this.Session.WriteLine($"s.limiti = {this.CurrentLevel}");
        }
        else
        {
            _ = this.Session.WriteLine($"s.func = {this.SMU}.{TspDevice.DCCurrentSourceFunction}");
            _ = this.Session.WriteLine($"s.limitv = {this.VoltageLevel}");
        }
        if (this.AutoRange)
        {
            _ = this.Session.WriteLine($"s.autorangei = {this.SMU}.AUTORANGE_ON");
            _ = this.Session.WriteLine($"s.autorangev = {this.SMU}.AUTORANGE_ON");
        }
        else
        {
            _ = this.Session.WriteLine($"s.autorangei = {this.SMU}.AUTORANGE_OFF");
            _ = this.Session.WriteLine($"s.rangei = {2 * this.CurrentLevel}");
            _ = this.Session.WriteLine($"s.rangev = {2 * this.VoltageLevel}");
        }


        _ = this.Session.WriteLine("*WAI");
        Thread.Sleep(100);
    }


    #endregion

    #region " Source Measure " 

    /// <summary>   Measure resistance. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool MeasureResistance()
    {
        bool success = false;
        _ = this.Session.WriteLine($"local s = {this.SMU}.source");
        _ = this.Session.WriteLine($"local m = {this.SMU}.measure");
        if (this.SourceFunction == TspDevice.DCVoltageSourceFunction)
        {
            _ = this.Session.WriteLine("s.levelv = 0");
            _ = this.Session.WriteLine($"s.output = {this.SMU}.OUTPUT_ON");
            _ = this.Session.WriteLine($"s.levelv = {this.VoltageLevel}");
        }
        else
        {
            _ = this.Session.WriteLine("s.leveli = 0");
            _ = this.Session.WriteLine($"s.output = {this.SMU}.OUTPUT_ON");
            _ = this.Session.WriteLine($"s.leveli = {this.CurrentLevel}");
        }
        string currentVoltageReading = string.Empty;
        int count= this.Session.QueryLine ( "_G.print(m.iv())",1024, ref currentVoltageReading, true);
        if (this.SourceFunction == TspDevice.DCVoltageSourceFunction)
        {
            _ = this.Session.WriteLine("s.levelv = 0");
        }
        else
        {
            _ = this.Session.WriteLine("s.leveli = 0");
        }
        _ = this.Session.WriteLine($"s.output = {this.SMU}.OUTPUT_OFF");

        this.ClearReadings();
        if ( count > 1 )
        {
            Queue<string> readingQueue = new(currentVoltageReading.Split(','));
            if ( readingQueue.Count > 1 ) 
            { 
                this.CurrentReading = readingQueue.Dequeue();
                if (double.TryParse(this.CurrentReading, out double v))
                {
                    this.CurrentValue = v;

                    if (double.TryParse(this.CurrentReading, out v))
                    {
                        this.VoltageValue = v;
                    }

                    if (this.CurrentValue > 0)
                    {
                        this.Resistance = this.VoltageLevel / this.CurrentLevel;
                        success = true;
                    }
                }
            }
        }
        return success;
    }

    #endregion

}
