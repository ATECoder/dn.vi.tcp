using cc.isr.Tcp.Client;

namespace cc.isr.Tcp.Tsp.Device.MSTest;

/// <summary>   (Unit Test Class) a session tests. </summary>
/// <remarks>   2024-02-05. </remarks>
[TestClass]
public class SessionTests
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private const string K7510IPAddress = "192.168.0.144";

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private const string K2600IPAddress = "192.168.0.150";

    private const string IPAddress = K2600IPAddress;

    /// <summary>   Assert identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="ipv4Address">  The IPv4 address. </param>
    /// <param name="repeatCount">  Number of repeats. </param>
    private static void AssertIdentityShouldQuery( string ipv4Address, int repeatCount )
    {
        using TcpSession session = new ( ipv4Address );
        string identity = string.Empty;
        string command = "*IDN?";
        bool trimEnd = true;
        session.Connect( true, command, ref identity, trimEnd );
        session.SendTimeout = TimeSpan.FromMilliseconds( 1000 );
        int count = repeatCount;
        while ( repeatCount > 0 )
        {
            repeatCount--;
            string response = string.Empty;
            _ = session.QueryLine( command, 1024, ref response, trimEnd ); 
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
        SessionTests.AssertIdentityShouldQuery( ipv4Address, count );
    }
}
