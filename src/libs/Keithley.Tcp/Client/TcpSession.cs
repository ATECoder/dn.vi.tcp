using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Keithley.Tcp.Client;

/// <summary>   A TCP session. </summary>
/// <remarks>   2022-11-14. </remarks>
public partial class TcpSession : IDisposable
{

    /// <summary>   Constructor. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="iPAddress">    Zero-based index of the p address. </param>
    /// <param name="portNumber">   (Optional) The port number. </param>
    public TcpSession( string iPAddress, int portNumber = 5025 )
    {
        this.PortNumber = portNumber;
        this.IPAddress = iPAddress;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    /// resources.
    /// </summary>
    /// <remarks>   2022-11-14. </remarks>
    public void Dispose()
    {
        this.Dispose( true );
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    /// resources.
    /// </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="disposing">    True to release both managed and unmanaged resources; false to
    ///                             release only unmanaged resources. </param>
    private void Dispose( bool disposing )
    {
        if ( disposing )
        {
            this._tcpClient?.Dispose();
            this._tcpClient = null;
            this._netStream = null;
        }
    }

    #region " TCP Client and Stream "

    private TcpClient _tcpClient = null;
    private NetworkStream _netStream = null;

    /// <summary>   Gets or sets the port number. </summary>
    /// <value> The port number. </value>
    public int PortNumber { get; private set; }

    /// <summary>   Gets or sets the IP address. </summary>
    /// <value> The IP address. </value>
    public string IPAddress { get; private set; }

    /// <summary>   Gets or sets the receive timeout. </summary>
    /// <remarks> Default value = 0 ms.</remarks>
    /// <value> The receive timeout. </value>
    public TimeSpan ReceiveTimeout
    {
        get => TimeSpan.FromMilliseconds( this._tcpClient.ReceiveTimeout );
        set => this._tcpClient.ReceiveTimeout = value.Milliseconds;
    }

    /// <summary>   Gets or sets the send timeout. </summary>
    /// <remarks> Default value = 0 ms.</remarks>
    /// <value> The send timeout. </value>
    public TimeSpan SendTimeout
    {
        get => TimeSpan.FromMilliseconds( this._tcpClient.SendTimeout );
        set => this._tcpClient.SendTimeout = value.Milliseconds;
    }

    /// <summary>   Gets or sets the size of the receive buffer. </summary>
    /// <remarks> Default value = 65536 </remarks>
    /// <value> The size of the receive buffer. </value>
    public int ReceiveBufferSize
    {
        get => this._tcpClient.ReceiveBufferSize;
        set => this._tcpClient.ReceiveBufferSize = value;
    }

    /// <summary>   Opens a new session. </summary>
    /// <remarks>   2022-11-14. </remarks>
    public void Connect()
    {
        this._tcpClient = new TcpClient( this.IPAddress, this.PortNumber );
        this._netStream = this._tcpClient.GetStream();
    }

    /// <summary>   Opens a new session. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="echoIdentity"> True to read the identity from the instrument. </param>
    public void Connect( bool echoIdentity )
    {
        this.Connect();
        if ( echoIdentity )
        {
            string _receiveBuffer = "";
            _ = this.QueryLine( "*IDN?", 128, ref _receiveBuffer );
        }
    }

    /// <summary>   Opens a new session. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="echoIdentity"> True to read the identity from the instrument. </param>
    /// <param name="identity">     [in,out] The identity text. </param>
    public void Connect( bool echoIdentity, ref string identity )
    {
        this.Connect();
        if ( echoIdentity )
        {
            _ = this.QueryLine( "*IDN?", 128, ref identity );
        }
    }

    /// <summary>   Disconnects this object. </summary>
    /// <remarks>   2022-11-14. </remarks>
    public void Disconnect()
    {
        this._netStream.Close();
        this._tcpClient.Close();
    }

    /// <summary>   Flushes the TCP Stream. </summary>
    /// <remarks>   2022-11-14. </remarks>
    public void Flush()
    {
        this._netStream.Flush();
    }

    #endregion

    #region " I/O "

    /// <summary>   Gets or sets the read termination. </summary>
    /// <value> The read termination. </value>
    public string ReadTermination { get; set; } = "\n";

    /// <summary>   Gets or sets the write termination. </summary>
    /// <value> The write termination. </value>
    public string WriteTermination { get; set; } = "\n";

    #endregion

    #region " SYNCHRONOUS I/O "

    /// <summary>
    /// Get a value indicating if data was received from the network and is available to be read.
    /// </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <returns>   True if data is available; otherwise, false . </returns>
    public bool DataAvailable()
    {
        return this._netStream.DataAvailable;
    }

    /// <summary>   Query if data was received from the network and is available to be read. </summary>
    /// <remarks>   2022-11-04. </remarks>
    /// <returns>   True if data is available; otherwise, false . </returns>
    public bool DataAvailable( TimeSpan timeout )
    {
        DateTime endTime = DateTime.Now.Add( timeout );
        while ( DateTime.Now < endTime && this.DataAvailable() )
        {
        }
        return this.DataAvailable();
    }

    /// <summary>   Writes a line. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="command">  The command. </param>
    /// <returns>   The number of sent characters. </returns>
    public int WriteLine( string command )
    {
        var task = this.SendAsync( command, this.CancellationToken );
        task.Wait();
        return task.Result;
        // byte[] buffer = Encoding.ASCII.GetBytes( $"{command}{this.WriteTermination}" );
        // this._netStream.Write( buffer, 0, buffer.Length );
        // return buffer.Length;
    }

    /// <summary>   Reads. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="reply">        [in,out] The reply. </param>
    /// <returns>   An int. </returns>
    public int Read( int byteCount, ref string reply )
    {
        byte[] buffer = new byte[byteCount];
        int bytesRcvd = this._netStream.Read( buffer, 0, byteCount );
        reply = Encoding.ASCII.GetString( buffer, 0, bytesRcvd );
        Array.Clear( buffer, 0, byteCount );
        return 0;
    }


    /// <summary>   Queries a line. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="command">      The command. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="reply">        [in,out] The reply. </param>
    /// <returns>   The line. </returns>
    public int QueryLine( string command, int byteCount, ref string reply )
    {
        _ = this.WriteLine( command );
        _ = this.Read( byteCount, ref reply );
        return 0;
    }

    /// <summary>   Receives. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="chunkSize">    Size of the chunk. </param>
    /// <param name="values">       [in,out] The values. </param>
    /// <returns>   An int. </returns>
    public int Receive( int chunkSize, ref float[] values )
    {
        byte[] buffer = new byte[chunkSize * 4 + 3];
        _ = this._netStream.Read( buffer, 0, buffer.Length );
        // Need to convert to the byte array into single or do
        Buffer.BlockCopy( buffer, 2, values, 0, values.Length * 4 );
        Array.Clear( buffer, 0, buffer.Length );
        return 0;
    }

    /// <summary>   Queries a line. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="command">      The command. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="values">       [in,out] The values. </param>
    /// <returns>   The line. </returns>
    public int QueryLine( string command, int byteCount, ref float[] values )
    {
        _ = this.WriteLine( command );
        _ = this.Receive( byteCount, ref values );
        return 0;
    }

    #endregion

    #region " ASYNCHRONOUS I/O "

    /// <summary>   Gets the cancellation token. </summary>
    /// <value> The cancellation token. </value>
    public CancellationToken CancellationToken { get; } = new CancellationToken();

    /// <summary>   Query if data was received from the network and is available to be read. </summary>
    /// <remarks>   2022-11-04. </remarks>
    /// <param name="timeout">  The timeout. </param>
    /// <returns>   True if data is available; otherwise, false . </returns>
    public async Task<bool> DataAvailableAsync( TimeSpan timeout )
    {
        return await Task<bool>.Run( () => this.DataAvailable( timeout ) );
    }

    /// <summary>   Receive asynchronously until no characters are available in the stream. </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="ct">           A token that allows processing to be canceled. </param>
    /// <returns>   A string. </returns>
    public async Task<string> ReceiveAsyncUntil( int byteCount, CancellationToken ct )
    {
        StringBuilder sb = new();
        while ( this._netStream.DataAvailable )
        {
            var buffer = new byte[byteCount];
            int bytesAvailable = await this._netStream.ReadAsync( buffer, 0, byteCount, ct );
            if ( bytesAvailable > 0 ) _ = sb.Append( Encoding.ASCII.GetString( buffer, 0, bytesAvailable ) );
        }
        return sb.ToString();
    }

    /// <summary>   Sends a message asynchronously reading any existing data into the orphan . </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="ct">       A token that allows processing to be canceled. </param>
    /// <returns>   The send. </returns>
    public async Task<int> SendAsync( string message, CancellationToken ct )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;

        // read any data already in the stream.
        this.Orphan = this._netStream.DataAvailable ? await this.ReceiveAsyncUntil( 2048, ct ) : string.Empty;

        byte[] buffer = Encoding.ASCII.GetBytes( $"{message}{this.WriteTermination}" );
        await this._netStream.WriteAsync( buffer, 0, buffer.Length, ct );
        this._netStream.Flush();
        return buffer.Length;
    }

    /// <summary>   Gets the last leftover response. </summary>
    /// <value> Any leftover message in the stream. </value>
    public string Orphan { get; private set; }


    #endregion
}
