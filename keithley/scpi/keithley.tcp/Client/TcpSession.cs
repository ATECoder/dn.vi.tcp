using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Keithley.Tcp.Client;

/// <summary>   A TCP session. </summary>
/// <remarks>   2022-11-14. </remarks>
public partial class TcpSession : IDisposable
{
    /// <summary>   Constructor. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="ipv4Address">  IPv4 Address in string format. </param>
    /// <param name="portNumber">   (Optional) The port number. </param>
    public TcpSession( string ipv4Address, int portNumber = 5025 )
    {
        this.PortNumber = portNumber;
        this.IPv4Address = ipv4Address;
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

    #region " tcp client and stream "

    private TcpClient _tcpClient = null;
    private NetworkStream _netStream = null;

    /// <summary>   Gets or sets the port number. </summary>
    /// <value> The port number. </value>
    public int PortNumber { get; private set; }

    /// <summary>   Gets or sets the IPv4 address. </summary>
    /// <value> The IP address. </value>
    public string IPv4Address { get; private set; }

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
        this._tcpClient = new TcpClient( this.IPv4Address, this.PortNumber );
        // notices that the Tcp Client is connected to the end point at this point
        // even though the .Connect command was not issued.
        this._netStream = this._tcpClient.GetStream();
    }

    /// <summary>   Begins a connect. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <returns>   An IAsyncResult. </returns>
    public IAsyncResult BeginConnect()
    {
        this._tcpClient = new TcpClient( );
        return  this._tcpClient.BeginConnect( this.IPv4Address, this.PortNumber, this.OnConnected, null );
    }

    /// <summary>   Await connect. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="asyncResult">  The result of the asynchronous operation. </param>
    /// <param name="timeout">      The timeout. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool AwaitConnect( IAsyncResult asyncResult, TimeSpan timeout )
    {
        var success = asyncResult.AsyncWaitHandle.WaitOne( timeout );
        if ( !success )
        {
            this._tcpClient.Close();
            this._tcpClient.EndConnect( asyncResult );
        }
        return success;
    }

    /// <summary>   Asynchronous callback, called on completion of on connected. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="asyncResult">  The result of the asynchronous operation. </param>
    private void OnConnected( IAsyncResult asyncResult  )
    {
        if ( asyncResult.IsCompleted )
        {
            this._netStream = this._tcpClient.GetStream();
        }
    }

    /// <summary>
    /// Opens a new session and reads the instrument identity to the debug console.
    /// </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="echoIdentity"> True to read the identity from the instrument. </param>
    /// <param name="queryMessage"> Message describing the query. </param>
    public void Connect( bool echoIdentity, string queryMessage )
    {
        this.Connect();
        if ( echoIdentity )
        {
            string identity = "";
            _ = this.QueryLine( queryMessage, 128, ref identity, false);
            System.Diagnostics.Debug.WriteLine( identity );
        }
    }


    /// <summary>
    /// Opens a new session and returns the instrument identity.
    /// </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="echoIdentity"> True to read the identity from the instrument. </param>
    /// <param name="queryMessage"> Message describing the query. </param>
    /// <param name="identity">     [in,out] The identity text. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    public void Connect( bool echoIdentity, string queryMessage, ref string identity, bool trimEnd )
    {
        this.Connect();
        if ( echoIdentity )
        {
            _ = this.QueryLine( queryMessage, 128, ref identity, trimEnd );
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

    #region " i/o "

    /// <summary>   Gets or sets the read termination. </summary>
    /// <value> The read termination. </value>
    public string ReadTermination { get; set; } = "\n";

    /// <summary>   Gets or sets the write termination. </summary>
    /// <value> The write termination. </value>
    public string WriteTermination { get; set; } = "\n";

    /// <summary>   Builds a reply. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="buffer">           The buffer. </param>
    /// <param name="receivedCount">    Number of received. </param>
    /// <param name="trimEnd">          True to trim the <see cref="ReadTermination"/>. </param>
    /// <returns>   A <see cref="string" />. </returns>
    private string BuildReply( byte[] buffer, int receivedCount, bool trimEnd )
    {
        int replyLength = receivedCount - (trimEnd ? this.ReadTermination.Length : 0);
        return replyLength > 0
            ? Encoding.ASCII.GetString( buffer, 0, replyLength )
            : string.Empty;
    }

    #endregion

    #region " synchronous i/o "

    /// <summary>
    /// Get a value indicating if data was received from the network and is available to be read.
    /// </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <returns>   True if data is available; otherwise, false . </returns>
    public bool DataAvailable()
    {
        return this._netStream.DataAvailable;
    }

    /// <summary>
    /// Query if data was received from the network and is available to be read during the <paramref name="timeout"/>
    /// period.
    /// </summary>
    /// <remarks>   2022-11-04. </remarks>
    /// <param name="timeout">  The timeout. </param>
    /// <returns>   True if data is available; otherwise, false . </returns>
    public bool DataAvailable( TimeSpan timeout )
    {
        DateTime endTime = DateTime.Now.Add( timeout );
        while ( DateTime.Now < endTime && this.DataAvailable() )
        {
        }
        return this.DataAvailable();
    }

    /// <summary>   Sends a message. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="message">  The message. </param>
    /// <returns>   The number of sent characters. </returns>
    public int Write( string message )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;
        byte[] buffer = Encoding.ASCII.GetBytes( message );
        this._netStream.Write( buffer, 0, buffer.Length );
        return buffer.Length;
    }

    /// <summary>   Sends a message with <see cref="WriteTermination"/>. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="message">  The message. </param>
    /// <returns>   The number of sent characters. </returns>
    public int WriteLine( string message )
    {
        return string.IsNullOrEmpty( message ) ? 0 : this.Write( $"{message}{this.WriteTermination}" );
    }

    /// <summary>   Reads the reply as a string. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="reply">        [in,out] The reply. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <returns>   The number of received characters. </returns>
    public int Read( int byteCount, ref string reply, bool trimEnd )
    {
        byte[] buffer = new byte[byteCount];
        int receivedCount = this._netStream.Read( buffer, 0, byteCount );
        reply = this.BuildReply(buffer, receivedCount, trimEnd);    
        return receivedCount;
    }

    /// <summary>   Sends a query message and reads the reply as a string. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="message">      The message. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="reply">        [in,out] The reply. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <returns>   An int. </returns>
    public int Query( string message, int byteCount, ref string reply, bool trimEnd )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;
        _ = this.Write( message );
        return this.Read( byteCount, ref reply, trimEnd );
    }

    /// <summary>   Sends a query message with termination and reads the reply as a string. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="message">      The message. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="reply">        [in,out] The reply. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <returns>   The number of received characters. </returns>
    public int QueryLine( string message, int byteCount, ref string reply, bool trimEnd )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;
        _ = this.WriteLine( message );
        return this.Read( byteCount, ref reply, trimEnd );
    }

    /// <summary>   Read single-precision values. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="offset">   The offset into the received bytes. </param>
    /// <param name="count">    Number of single precision values. </param>
    /// <param name="values">   [in,out] the single precision values. </param>
    /// <returns>   The number of received bytes. </returns>
    public int Read( int offset, int count, ref float[] values )
    {
        byte[] buffer = new byte[count * 4 + offset + 1];
        int receivedCount = this._netStream.Read( buffer, 0, buffer.Length );
        // Need to convert to the byte array into single
        Buffer.BlockCopy( buffer, offset, values, 0, values.Length * 4 );
        return receivedCount;
    }

    /// <summary>   Sends a query message and reads the reply as a single-precision values. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="offset">   The offset into the received bytes. </param>
    /// <param name="count">    Number of single precision values. </param>
    /// <param name="values">   [in,out] the single precision values. </param>
    /// <returns>   The number of received bytes. </returns>
    public int Query( string message, int offset, int count, ref float[] values )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;
        _ = this.Write( message );
        return this.Read( offset, count, ref values );
    }

    /// <summary>   Sends a query message with termination and reads the reply as a single-precision values. </summary>
    /// <remarks>   2022-11-14. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="offset">   The offset into the received bytes. </param>
    /// <param name="count">    Number of single precision values. </param>
    /// <param name="values">   [in,out] the single precision values. </param>
    /// <returns>   The number of received bytes. </returns>
    public int QueryLine( string message, int offset, int count, ref float[] values )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;
        _ = this.WriteLine( message );
        return this.Read( offset, count, ref values );
    }

    #endregion

    #region " asynchronous i/o "

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

    /// <summary>   Read asynchronously until no characters are available in the stream. </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <param name="ct">           A token that allows processing to be canceled. </param>
    /// <returns>   A reply. </returns>
    public async Task<string> ReadWhileAvailableAsync( int byteCount, bool trimEnd, CancellationToken ct )
    {
        StringBuilder sb = new();
        while ( this._netStream.DataAvailable )
        {
            var buffer = new byte[byteCount];
            int receivedCount = await this._netStream.ReadAsync( buffer, 0, byteCount, ct );
            if ( receivedCount > 0 ) _ = sb.Append( Encoding.ASCII.GetString( buffer, 0, receivedCount ) );
        }
        int replyLength = sb.Length - ( trimEnd ? this.ReadTermination.Length : 0 );
        return replyLength > 0
            ? sb.ToString( 0, replyLength )
            : string.Empty;
    }

    /// <summary>
    /// Read asynchronously data that was already received from the network and is available to be
    /// read.
    /// </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <param name="ct">           A token that allows processing to be canceled. </param>
    /// <returns>   A reply. </returns>
    public async Task<string> ReadAsync( int byteCount, bool trimEnd, CancellationToken ct )
    {
        var buffer = new byte[byteCount];
        int receivedCount = await this._netStream.ReadAsync( buffer, 0, byteCount, ct );
        return  this.BuildReply( buffer, receivedCount, trimEnd );
    }

    /// <summary>   Sends a message asynchronously reading any existing data into the orphan . </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="ct">       A token that allows processing to be canceled. </param>
    /// <returns>   The number of sent characters. </returns>
    public async Task<int> WriteAsync( string message, CancellationToken ct )
    {
        if ( string.IsNullOrEmpty( message ) ) return 0;

        // read any data already in the stream.
        this.Orphan = this._netStream.DataAvailable
            ? await this.ReadWhileAvailableAsync( 2048, false, ct )
            : string.Empty;

        byte[] buffer = Encoding.ASCII.GetBytes( message );
        await this._netStream.WriteAsync( buffer, 0, buffer.Length, ct );
        return buffer.Length;
    }

    /// <summary>   Sends a message with termination asynchronously reading any existing data into the orphan . </summary>
    /// <remarks>   2022-11-15. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="ct">       A token that allows processing to be canceled. </param>
    /// <returns>   The number of sent characters. </returns>
    public async Task<int> WriteLineAsync( string message, CancellationToken ct )
    {
        return string.IsNullOrEmpty( message ) ? 0 : await this.WriteAsync( $"{message}{this.WriteTermination}" , ct );
    }

    /// <summary>   Sends a query message with termination and reads the reply as a string. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="message">      The message. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="readDelay">    The read delay. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <param name="ct">           A token that allows processing to be canceled. </param>
    /// <returns>   A reply. </returns>
    public async Task<string> QueryAsync( string message, int byteCount, TimeSpan readDelay, bool trimEnd, CancellationToken ct )
    {
        if ( string.IsNullOrEmpty( message ) ) return string.Empty;

        Task<int> sendTask = this.WriteAsync( message, ct );
        sendTask.Wait();

        // wait for available data.
        Task<bool> delayTask = this.DataAvailableAsync( readDelay );
        delayTask.Wait();

        // we ignore the delay task result in order to simplify the code as this
        // would return no data if the stream has no available data.
        return await this.ReadAsync( byteCount, trimEnd, ct );
    }

    /// <summary>   Queries line asynchronous. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="message">      The message. </param>
    /// <param name="byteCount">    Number of bytes. </param>
    /// <param name="readDelay">    The read delay. </param>
    /// <param name="trimEnd">      True to trim the <see cref="ReadTermination"/>. </param>
    /// <param name="ct">           A token that allows processing to be canceled. </param>
    /// <returns>   A reply. </returns>
    public async Task<string> QueryLineAsync( string message, int byteCount, TimeSpan readDelay, bool trimEnd, CancellationToken ct )
    {
        return string.IsNullOrEmpty( message )
            ? string.Empty
            : await this.QueryAsync( $"{message}{this.WriteTermination}", byteCount, readDelay, trimEnd, ct );
    }

    /// <summary>   Gets the last leftover response. </summary>
    /// <value> Any leftover message in the stream. </value>
    public string Orphan { get; private set; }

    #endregion
}
