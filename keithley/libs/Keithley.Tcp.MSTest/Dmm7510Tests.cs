using Keithley.Tcp.Client;

namespace Keithley.Tcp.MSTest;

[TestClass]
public class Dmm7510Tests
{

    /// <summary>   Assert identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    /// <param name="ipAddress">    The IP address. </param>
    /// <param name="repeatCount">  Number of repeats. </param>
    private void AssertIdentityShouldQuery( string ipAddress, int repeatCount )
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
            string respnonse = string.Empty;
            session.QueryLine( command, 1024 , ref respnonse, trimEnd ); 
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
        this.AssertIdentityShouldQuery(ipAddress, count );
    }
}
