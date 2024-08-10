namespace cc.isr.Tcp.Tsp.Device.MSTest;

/// <summary>   (Unit Test Class) a session tests. </summary>
/// <remarks>   2024-02-05. </remarks>
[TestClass]
public class TspDeviceTests
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private const string K7510IPAddress = "192.168.0.144";

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    private const string K2600IPAddress = "192.168.0.150";

    private const string IPAddress = K2600IPAddress;

    /// <summary>   Assert device should connect. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">  The IPv4 address. </param>
    private static void AssertDeviceShouldConnect( string ipv4Address )
    {
        using TspDevice session = new( ipv4Address );
        string identity = string.Empty;
        session.Connect( true, ref identity );
        Assert.IsTrue( identity.Length > 0, "Echoed Identity should have non-zero length." );
        Assert.IsTrue( identity.StartsWith( "Keithley", StringComparison.InvariantCultureIgnoreCase ), "Identity should start with the manufacturer name." );

        string request = "_G.localnode.showerrors";
        string showErrorsExpected = "0.00000e+00";
        string showErrorsActual = session.QueryDeviceInfo( request );
        Assert.AreEqual( showErrorsExpected, showErrorsActual, $"{request} should equal '{showErrorsExpected}'" );

        request = "_G.localnode.prompts";
        string promptsExpected = "0.00000e+00";
        string promptsActual = session.QueryDeviceInfo( request );
        Assert.AreEqual( promptsExpected, promptsActual, $"{request} should equal '{promptsExpected}'" );

        session.ResetKnownState();

        session.Disconnect();
    }

    /// <summary>   (Unit Test Method) identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void DeviceShouldConnect()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldConnect( ipv4Address );
    }

    /// <summary>   Assert device should query info. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">      The IPv4 address. </param>
    /// <param name="request">   Requested info. </param>
    public static void AssertDeviceShouldQueryInfo( string ipv4Address, string request, string expectedReply )
    {
        using TspDevice session = new( ipv4Address );
        string identity = string.Empty;
        session.Connect( true, ref identity );
        Assert.IsTrue( identity.Length > 0, "Echoed Identity should have non-zero length." );
        Assert.IsTrue( identity.StartsWith( "Keithley", StringComparison.InvariantCultureIgnoreCase ), "Identity should start with the manufacturer name." );
        string reply = session.QueryDeviceInfo( request );
        Assert.AreEqual( expectedReply, reply );
#if false
        _ = session.Session.WriteLine($"m = _G.smua.measure");
        reply = session.QueryDeviceInfo("m");
#endif
        session.Disconnect();
    }

    [TestMethod]
    public void DeviceShouldQuerySmuAutoRangeOnValue()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldQueryInfo( ipv4Address, "_G.smua.AUTORANGE_ON", "1.00000e+00" );
    }

    /// <summary>   Assert device should configure current source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">      The IPv4 address. </param>
    /// <param name="request">   Source function. </param>
    public static void AssertDeviceShouldConfigureSource( string ipv4Address, string sourceFunction )
    {
        using TspDevice session = new( ipv4Address );
        string identity = string.Empty;
        session.Connect( true, ref identity );
        Assert.IsTrue( identity.Length > 0, "Echoed Identity should have non-zero length." );
        Assert.IsTrue( identity.StartsWith( "Keithley", StringComparison.InvariantCultureIgnoreCase ), "Identity should start with the manufacturer name." );
        session.SourceFunction = sourceFunction;
        session.ConfigureConstantSource();
        session.Disconnect();
    }

    /// <summary>   (Unit Test Method) device should configure current source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    [TestMethod]
    public void DeviceShouldConfigureCurrentSource()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldConfigureSource( ipv4Address, cc.isr.Tcp.Tsp.Device.TspDevice.DCCurrentSourceFunction );
    }

    /// <summary>   (Unit Test Method) device should configure voltage source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    [TestMethod]
    public void DeviceShouldConfigureVoltageSource()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldConfigureSource( ipv4Address, cc.isr.Tcp.Tsp.Device.TspDevice.DCVoltageSourceFunction );
    }

    /// <summary>   Assert device should measure resistance. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">      The IPv4 address. </param>
    /// <param name="sourceFunction">   Source function. </param>
    public static void AssertDeviceShouldMeasureResistance( string ipv4Address, string sourceFunction )
    {
        Console.WriteLine( $"Connecting to instrument at {ipv4Address}......" );
        using TspDevice session = new( ipv4Address );
        string identity = string.Empty;
        session.Connect( true, ref identity );
        Assert.IsTrue( identity.Length > 0, "Echoed Identity should have non-zero length." );
        Assert.IsTrue( identity.StartsWith( "Keithley", StringComparison.InvariantCultureIgnoreCase ), "Identity should start with the manufacturer name." );
        session.SourceFunction = sourceFunction;
        Console.WriteLine( $"configuring......" );
        session.ConfigureConstantSource();
        Console.WriteLine( $"measuring......" );
        _ = session.MeasureResistance();
        double lowLimit = 1.8;
        double highLimit = 2.2;
        Assert.IsTrue( session.Resistance.HasValue, "Resistance should be measured." );
        Assert.IsTrue( session.Resistance.Value > lowLimit, $"Resistance {session.Resistance.Value} should be above {lowLimit} ohms" );
        Assert.IsTrue( session.Resistance.Value < highLimit, $"Resistance {session.Resistance.Value} should be below {highLimit} ohms" );
        Console.WriteLine( $"R={session.Resistance.Value}" );

        session.Disconnect();
    }

    [TestMethod]
    public void DeviceShouldMeasureConstantCurrentResistance()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldMeasureResistance( ipv4Address, cc.isr.Tcp.Tsp.Device.TspDevice.DCCurrentSourceFunction );
    }

    [TestMethod]
    public void DeviceShouldMeasureConstantVoltageResistance()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldMeasureResistance( ipv4Address, cc.isr.Tcp.Tsp.Device.TspDevice.DCVoltageSourceFunction );
    }

}
