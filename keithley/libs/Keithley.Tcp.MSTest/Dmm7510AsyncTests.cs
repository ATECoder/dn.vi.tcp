using System.Diagnostics;

using Keithley.Tcp.Client;

namespace Keithley.Tcp.MSTest;

[TestClass]
public class Dmm7510AsyncTests
{

    /// <summary>   Writes a line. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="session">  The session. </param>
    /// <param name="command">  The command. </param>
    /// <returns>   An int. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private static int WriteLine( TcpSession session, string command )
    {
        var task = session.WriteLineAsync( command, session.CancellationToken );
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
        var task = session.QueryLineAsync( command, 1024, readDelay, trimEnd, session.CancellationToken );
        task.Wait();
        return task.Result;
    }

    /// <summary>   Assert identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="ipAddress">    The IP address. </param>
    /// <param name="repeatCount">  Number of repeats. </param>
    private static void AssertIdentityShouldQuery( string ipAddress, TimeSpan readDelay, int repeatCount )
    {
        using TcpSession session = new ( ipAddress );
        string identity = string.Empty;
        string command = "*IDN?";
        bool trimEnd = true;
        session.Connect( true, command, ref identity, trimEnd );
        session.SendTimeout = TimeSpan.FromMilliseconds( 1000 );
        int count = repeatCount;
        while ( repeatCount > 0 )
        {
            repeatCount--;
            string respnonse = QueryLine( session, command, readDelay, trimEnd ); 
            Assert.AreEqual( identity, respnonse, $"@count = {count - repeatCount}" );
        }
    }

    /// <summary>   (Unit Test Method) identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void IdentityShouldQuery()
    {
        string ipAddress = "192.168.0.144";
        int count = 42;
        TimeSpan readDelay = TimeSpan.FromMilliseconds( 0 );
        AssertIdentityShouldQuery(ipAddress, readDelay, count );
    }

    /// <summary>   (Unit Test Method) session TCP client should begin connect. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void SessionTcpClientShouldBeginConnect()
    {
        string ipAddress = "192.168.0.144";
        TimeSpan timeout = TimeSpan.FromMilliseconds( 100 );
        using TcpSession session = new( ipAddress );
        Stopwatch sw = Stopwatch.StartNew();
        IAsyncResult asyncResult = session.BeginConnect();
        bool success = session.AwaitConnect( asyncResult, TimeSpan.FromMilliseconds( 100 ) );
        Assert.IsTrue( success , $"connected to {ipAddress} failed after {timeout.TotalMilliseconds:0}ms");
        sw.Stop();
        Console.WriteLine(
            $"Connected in {sw.ElapsedTicks}/{TimeSpan.TicksPerMillisecond} {((double)sw.ElapsedTicks / TimeSpan.TicksPerMillisecond):0.0}ms" );
    }
}
