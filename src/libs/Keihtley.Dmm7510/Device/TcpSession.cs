using System;
using System.Net.Sockets;
using System.Text;

namespace Keithley.Dmm7510.Device;

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

    /// <summary>   Writes a line. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="command">  The command. </param>
    /// <returns>   The number of sent characters. </returns>
    public int WriteLine( string command )
    {
        byte[] buffer = Encoding.ASCII.GetBytes( $"{command}{this.WriteTermination}" );
        this._netStream.Write( buffer, 0, buffer.Length );
        return buffer.Length;
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

    #endregion
}
