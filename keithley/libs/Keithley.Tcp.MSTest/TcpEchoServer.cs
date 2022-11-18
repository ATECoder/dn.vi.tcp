using System.Net.Sockets;
using System.Net;

namespace Keithley.Tcp.MSTest;


/// <summary>   A TCP echo server. </summary>
/// <remarks>   2022-11-16. </remarks>
public class TcpEchoServer : ObservableObject
{

    public TcpEchoServer( string ipString = "127.0.0.1", int portNumber = 13000 )
    {
        this.Port = portNumber;
        this._ipv4Address = "";
        this._message = "";
        this.IPv4Address =  ipString?? "127.0.0.1";
    }

    private bool _running;
    public bool Running
    {
        get => this._running;
        set => _ = this.OnPropertyChanged( ref this._running, value );
    }


    private int _port;
    public int Port
    {
        get => this._port;
        set => _ = this.OnPropertyChanged( ref this._port, value );
    }

    private string _ipv4Address;
    public string IPv4Address
    {
        get => this._ipv4Address;
        set => _ = this.OnPropertyChanged( ref this._ipv4Address, value );
    }

    private string _message;
    public string Message
    {
        get => this._message;
        set => _ = this.OnPropertyChanged( ref this._message, value );
    }

    /// <summary>   Listen asynchronously. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <returns>   A Task. </returns>
    public async Task ListenAsync()
    {
        await Task.Run( () => { this.Listen(); } );
    }

    /// <summary>   Start listening for incoming connections. </summary>
    /// <remarks>   2022-11-16. </remarks>
    public void Listen()
    {
        try
        {
            // Set the TcpListener on port 13000.
            Int32 port = this.Port;
            IPAddress localAddr = IPAddress.Parse( this.IPv4Address );
            int receivedBufferLength = 256;

            // TcpListener server = new TcpListener(port);
            TcpListener server = new TcpListener( localAddr, port );

            try
            {
                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] receiveBuffer = new Byte[receivedBufferLength];

                // Enter the listening loop.
                this.Running = true;
                while ( this.Running )
                {
                    this.Message = $"{nameof( TcpEchoServer )} is waiting for a connection... ";

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = server.AcceptTcpClient();
                    this.Message = "client Connected!";

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int receivedCount;

                    // Loop to receive all the data sent by the client.
                    while ( (receivedCount = stream.Read( receiveBuffer, 0, receiveBuffer.Length )) != 0 )
                    {
                        // Translate data bytes to a ASCII string.
                        string receivedChunk = System.Text.Encoding.ASCII.GetString( receiveBuffer, 0, receivedCount );
                        this.Message = $"Received: '{receivedChunk}'";

                        // Process the data sent by the client.
                        receivedChunk = receivedChunk.ToUpper();

                        byte[] writingBuffer = System.Text.Encoding.ASCII.GetBytes( receivedChunk );

                        // Send back a response.
                        stream.Write( writingBuffer, 0, writingBuffer.Length );
                        this.Message = $"Sent: '{receivedChunk}'";
                    }
                }
            }
            catch ( Exception ex )
            {
                this.Message = $"SocketException: {ex}";
            }
            finally
            {
                server.Stop();
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
