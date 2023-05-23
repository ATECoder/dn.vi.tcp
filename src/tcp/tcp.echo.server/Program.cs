// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;

try
{
    // Set the TcpListener on port 13000.
    Int32 port = 13000;
    IPAddress localAddr = IPAddress.Parse( "127.0.0.1" );
    int readingBufferLength = 256;

    // TcpListener server = new TcpListener(port);
    TcpListener server = new( localAddr, port );

    try
    {
        // Start listening for client requests.
        server.Start();

        // Buffer for reading data
        Byte[] readingBuffer = new Byte[readingBufferLength];

        // Enter the listening loop.
        while ( !Console.KeyAvailable )
        {
            Console.Write( "Waiting for a connection... " );

            // Perform a blocking call to accept requests.
            // You could also use server.AcceptSocket() here.
            using TcpClient client = server.AcceptTcpClient();
            Console.WriteLine( "Connected!" );

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int receivedCount;

            // Loop to receive all the data sent by the client.
            while ( (receivedCount = stream.Read( readingBuffer, 0, readingBuffer.Length )) != 0 )
            {
                // Translate data bytes to a ASCII string.
                string receivedChunk = System.Text.Encoding.ASCII.GetString( readingBuffer, 0, receivedCount );
                Console.WriteLine( $"Received: '{receivedChunk}'" );

                // Process the data sent by the client.
                receivedChunk = receivedChunk.ToUpper();

                byte[] writingBuffer = System.Text.Encoding.ASCII.GetBytes( receivedChunk );

                // Send back a response.
                stream.Write( writingBuffer, 0, writingBuffer.Length );
                Console.WriteLine( $"Sent: '{receivedChunk}'"  );
            }
        }

    }
    catch ( Exception ex )
    {
        Console.WriteLine( $"Exception: {ex}" );
    }
    finally
    {
        server.Stop();
    }

}
catch ( Exception ex )
{
    Console.WriteLine( $"Exception: {ex}" );
}
finally
{
}
Console.WriteLine( "\nHit enter to continue..." );
Console.Read();
