using System.Net.Sockets;
using System.Net;

namespace cc.isr.Tcp.Client.MSTest;

/// <summary>   A TCP echo server. </summary>
/// <remarks>   2022-11-16. </remarks>
public class TcpEchoServer : ObservableObject, IDisposable
{
    #region " construction and cleanup "

    public TcpEchoServer( string ipString = "127.0.0.1", int portNumber = 13000 )
    {
        this.Port = portNumber;
        this._ipv4Address = "";
        this._message = "";
        this.IPv4Address = ipString ?? "127.0.0.1";
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    /// resources.
    /// </summary>
    /// <remarks>
    /// Takes account of and updates <see cref="IsDisposed"/>. Encloses <see cref="Dispose(bool)"/>
    /// within a try...finally block. <para>
    ///
    /// Because this class is implementing <see cref="IDisposable"/> and is not sealed, then it
    /// should include the call to <see cref="GC.SuppressFinalize(object)"/> even if it does not
    /// include a user-defined finalizer. This is necessary to ensure proper semantics for derived
    /// types that add a user-defined finalizer but only override the protected <see cref="Dispose(bool)"/>
    /// method. </para> <para>
    /// 
    /// To this end, call <see cref="GC.SuppressFinalize(object)"/>, where <see langword="Object"/> = <see langword="this"/> in the <see langword="Finally"/> segment of
    /// the <see langword="try"/>...<see langword="catch"/> clause. </para><para>
    ///
    /// If releasing unmanaged code or freeing large objects then override <see cref="object.Finalize()"/>. </para>
    /// </remarks>
    public void Dispose()
    {
        if ( this.IsDisposed ) { return; }
        try
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.

            this.Dispose( true );

        }
        catch { throw; }
        finally
        {
            // this is included because this class is not sealed.

            GC.SuppressFinalize( this );

            // mark things as disposed.

            this.IsDisposed = true;
        }
    }

    /// <summary>   Gets or sets a value indicating whether this object is disposed. </summary>
    /// <value> True if this object is disposed, false if not. </value>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Releases unmanaged, large objects and (optionally) managed resources used by this class.
    /// </summary>
    /// <param name="disposing">    True to release large objects and managed and unmanaged resources;
    ///                             false to release only unmanaged resources and large objects. </param>
    protected virtual void Dispose( bool disposing )
    {
        if ( disposing )
        {
            // dispose managed state (managed objects)
            this._listener?.Dispose();
        }

        // free unmanaged resources and override finalizer

        // set large fields to null
    }

    #endregion

    private bool _listening;
    /// <summary>
    /// Gets or sets a value indicating whether the listener is listening to incoming requests.
    /// </summary>
    /// <value> True if listening, false if not. </value>
    public bool Listening
    {
        get => this._listening;
        private set => _ = this.OnPropertyChanged( ref this._listening, value );
    }

    private int _port;
    /// <summary>   Gets or sets the port. </summary>
    /// <value> The port. </value>
    public int Port
    {
        get => this._port;
        set => _ = this.OnPropertyChanged( ref this._port, value );
    }

    private string _ipv4Address;
    /// <summary>   Gets or sets the IPv4 address. </summary>
    /// <value> The IPv4 address. </value>
    public string IPv4Address
    {
        get => this._ipv4Address;
        set => _ = this.OnPropertyChanged( ref this._ipv4Address, value );
    }

    private string _message;
    /// <summary>   Gets or sets the message. </summary>
    /// <value> The message. </value>
    public string Message
    {
        get => this._message;
        set => _ = this.OnPropertyChanged( ref this._message, value );
    }

    /// <summary>
    /// The TCP client connected thread synchronization event that, when signaled, must be reset
    /// manually.
    /// </summary>
    private static readonly ManualResetEvent _tcpClientConnected = new( false );

    private TcpListener? _listener;
    /// <summary>   Starts listening for client connections. </summary>
    /// <remarks>   2022-11-17. </remarks>
    public void Start()
    {
        // TcpListener server = new TcpListener(port);
        this._listener = new TcpListener( IPAddress.Parse( this.IPv4Address ), this.Port );

        // Start listening for client requests.
        this._listener.Start();

        this.Listening = true;

        // Start to listen for connections from a client.
        this.Message = $"{nameof( TcpEchoServer )} is waiting for a connection... ";

        // Accept the connection.
        _ = this._listener.BeginAcceptTcpClient( new AsyncCallback( this.DoAcceptTcpClientCallback ), this._listener );

        // accept one client connection asynchronously
        // this.DoBeginAcceptTcpClient( this._listener );
    }

    /// <summary>   Stops listening. </summary>
    /// <remarks>   2022-11-17. </remarks>
    public void Stop()
    {
        this.Message = $"Stopping the {nameof( TcpEchoServer )}. ";
        this.Listening = false;
        this._listener?.Stop();
    }

    /// <summary>   Accept one client connection asynchronously. </summary>
    /// <remarks>   2022-11-17. </remarks>
    /// <param name="listener"> The listener. </param>
    public void DoBeginAcceptTcpClient( TcpListener listener )
    {
        // Set the event to non-signaled state.
        _ = _tcpClientConnected.Reset();

        // Start to listen for connections from a client.
        this.Message = $"{nameof( TcpEchoServer )} is waiting for a connection... ";

        // Accept the connection.
        _ = listener.BeginAcceptTcpClient( new AsyncCallback( this.DoAcceptTcpClientCallback ), listener );

        // Wait until a connection is made and processed before continuing.
        _ = _tcpClientConnected.WaitOne();
    }


    /// <summary>
    /// Asynchronous callback, called on accepting the TCP client connection. Processes the client
    /// request.
    /// </summary>
    /// <remarks>   2022-11-17. </remarks>
    /// <param name="asyncResult">  The result of the asynchronous operation. </param>
    private void DoAcceptTcpClientCallback( IAsyncResult asyncResult )
    {
        // Get the listener that handles the client request.
        TcpListener listener = ( TcpListener ) asyncResult.AsyncState!;

        // check listener.Server.IsBound in the async callback and if it’s false, 
        // just return. No need to call EndAcceptTcpClient and then catch the
        // (expected and documented) exception. 
        // While the socket is listening IsBound is set to true;
        // After you call close it's value will be false. 
        if ( listener is not null && listener.Server.IsBound )
        {
            // End the operation.
            TcpClient client = listener.EndAcceptTcpClient( asyncResult );

            // Process the connection here. (Add the client to a
            // server table, read data, etc.)
            this.ProcessClientRequest( client );

            // Signal the calling thread to continue.
            _ = _tcpClientConnected.Set();

            // continue listening.
            this.DoBeginAcceptTcpClient( listener );
        }
    }

    /// <summary>
    /// Process the client request, e.g., add the client to a server table, read data.
    /// </summary>
    /// <remarks>   2022-11-17. </remarks>
    /// <param name="client">   The client. </param>
    private void ProcessClientRequest( TcpClient client )
    {

        this.Message = "processing request...";

        int receivedBufferLength = 256;

        // Get a stream object for reading and writing
        NetworkStream stream = client.GetStream();

        // Buffer for reading data
        byte[] receiveBuffer = new byte[receivedBufferLength];

        int receivedCount;

        // Loop to receive all the data sent by the client.
        while ( (receivedCount = stream.Read( receiveBuffer, 0, receiveBuffer.Length )) != 0 )
        {
            // Translate data bytes to a ASCII string.
            string receivedChunk = System.Text.Encoding.ASCII.GetString( receiveBuffer, 0, receivedCount );
            this.Message = $"Received: '{receivedChunk}'";

            // Process the data sent by the client.
            receivedChunk = receivedChunk.ToUpperInvariant();

            byte[] writingBuffer = System.Text.Encoding.ASCII.GetBytes( receivedChunk );

            // Send back a response.
            stream.Write( writingBuffer, 0, writingBuffer.Length );
            this.Message = $"Sent: '{receivedChunk}'";
        }
    }

    /// <summary>   Listen asynchronously. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <returns>   A Task. </returns>
    public async Task ListenAsync()
    {
        await Task.Run( this.Listen );
        // await Task.Run( () => { this.Listen(); } );
    }

    /// <summary>   Start listening for incoming connections. </summary>
    /// <remarks>   2022-11-16. </remarks>
    public void Listen()
    {
        try
        {

            TcpListener listener = new( IPAddress.Parse( this.IPv4Address ), this.Port );

            try
            {
                // Start listening for client requests.
                listener.Start();

                // Enter the listening loop.
                this.Listening = true;
                while ( this.Listening )
                {
                    this.Message = $"{nameof( TcpEchoServer )} is waiting for a connection... ";

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = listener.AcceptTcpClient();

                    this.ProcessClientRequest( client );
                }
            }
            catch ( Exception ex )
            {
                this.Message = $"SocketException: {ex}";
            }
            finally
            {
                listener.Stop();
            }

        }
        catch ( Exception ex )
        {
            this.Message = $"Exception: {ex}";
        }
        finally
        {
        }

        this.Message = $"{nameof( TcpEchoServer )} exited";
    }
}
