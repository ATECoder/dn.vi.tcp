using System.Diagnostics;
using cc.isr.Tcp.Client;

namespace cc.isr.Tcp.Tsp.Device.MSTest;

/// <summary>   (Unit Test Class) a session asynchronous tests. </summary>
/// <remarks>   2024-02-05. </remarks>
[TestClass]
public class SessionAsyncTests
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private const string K7510IPAddress = "192.168.0.144";

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private const string K2600IPAddress = "192.168.0.150";

    private const string IPAddress = K2600IPAddress;

    /// <summary>   Writes a line. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="session">  The session. </param>
    /// <param name="command">  The command. </param>
    /// <returns>   An int. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private static int WriteLine( TcpSession session, string command )
    {
        Task<int> task = session.WriteLineAsync( command, session.CancellationToken );
        task.Wait();
        return task.Result;
    }

    /// <summary>   Queries a line. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="session">  The session. </param>
    /// <param name="command">  The command. </param>
    /// <param name="trimEnd">  True to trim end. </param>
    /// <returns>   The line. </returns>
    private static string QueryLine( TcpSession session, string command, TimeSpan readDelay, bool trimEnd )
    {
        Task<string> task = session.QueryLineAsync( command, 1024, readDelay, trimEnd, session.CancellationToken );
        task.Wait();
        return task.Result;
    }

    /// <summary>   Assert identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="ipv4Address">  The IPv4 address. </param>
    /// <param name="readDelay">    The read delay. </param>
    /// <param name="repeatCount">  Number of repeats. </param>
    private static void AssertIdentityShouldQuery( string ipv4Address, TimeSpan readDelay, int repeatCount )
    {
        using TcpSession session = new( ipv4Address );
        string identity = string.Empty;
        string command = "*IDN?";
        bool trimEnd = true;
        session.Connect( true, command, ref identity, trimEnd );
        session.SendTimeout = TimeSpan.FromMilliseconds( 1000 );
        int count = repeatCount;
        while ( repeatCount > 0 )
        {
            repeatCount--;
            string response = QueryLine( session, command, readDelay, trimEnd );
            Assert.AreEqual( identity, response, $"@count = {count - repeatCount}" );
        }
    }

    /// <summary>   (Unit Test Method) identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void IdentityShouldQuery()
    {
        string ipv4Address = IPAddress;
        int count = 42;
        TimeSpan readDelay = TimeSpan.FromMilliseconds( 0 );
        AssertIdentityShouldQuery( ipv4Address, readDelay, count );
    }

    /// <summary>   (Unit Test Method) session TCP client should begin connect. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void SessionTcpClientShouldBeginConnect()
    {
        string ipv4Address = IPAddress;
        TimeSpan timeout = TimeSpan.FromMilliseconds( 100 );
        using TcpSession session = new( ipv4Address );
        Stopwatch sw = Stopwatch.StartNew();
        IAsyncResult asyncResult = session.BeginConnect();
        bool success = session.AwaitConnect( asyncResult, TimeSpan.FromMilliseconds( 100 ) );
        Assert.IsTrue( success, $"connected to {ipv4Address} failed after {timeout.TotalMilliseconds:0}ms" );
        sw.Stop();
        Console.WriteLine(
            $"Connected in {sw.ElapsedTicks}/{TimeSpan.TicksPerMillisecond} {( double ) sw.ElapsedTicks / TimeSpan.TicksPerMillisecond:0.0}ms" );
    }
}
