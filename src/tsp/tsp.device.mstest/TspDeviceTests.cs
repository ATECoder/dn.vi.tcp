using Microsoft.ApplicationInsights;

namespace cc.isr.Tcp.Tsp.Device.MSTest;

/// <summary>   (Unit Test Class) a session tests. </summary>
/// <remarks>   2024-02-05. </remarks>
[TestClass]
public class TspDeviceTests
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private const string K7510IPAddress = "192.168.0.144";

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private const string K2600IPAddress = "192.168.0.150";

    private const string IPAddress = K2600IPAddress;

    /// <summary>   Assert device should connect. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">  The IPv4 address. </param>
    private static void AssertDeviceShouldConnect( string ipv4Address )
    {
        using TspDevice session = new ( ipv4Address );
        string identity = string.Empty;
        session.Connect( true, ref identity);
        Assert.IsTrue( identity.Length > 0, "Echoed Identity should have non-zero length." );
        Assert.IsTrue(identity.StartsWith("Keithley"), "Identity should start with the manufacturer name.");

        string request = "_G.localnode.showerrors";
        string showErrorsExpected = "0.00000e+00";
        string showErrorsActual = session.QueryDeviceInfo(request);
        Assert.AreEqual(showErrorsExpected, showErrorsActual, $"{request} should equal '{showErrorsExpected}'");

        request = "_G.localnode.prompts";
        string promptsExpected = "0.00000e+00";
        string promptsActual = session.QueryDeviceInfo(request);
        Assert.AreEqual(promptsExpected, promptsActual, $"{request} should equal '{promptsExpected}'");

        session.ResetKnownState();

        session.Disconnect();
    }

    /// <summary>   (Unit Test Method) identity should query. </summary>
    /// <remarks>   2022-11-16. </remarks>
    [TestMethod]
    public void DeviceShouldConnect()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldConnect( ipv4Address);
    }

    /// <summary>   Assert device should configure current source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">      The IPv4 address. </param>
    /// <param name="sourceFunction">   Source function. </param>
    public static void AssertDeviceShouldConfigureSource(string ipv4Address, string sourceFunction)
    {
        using TspDevice session = new(ipv4Address);
        string identity = string.Empty;
        session.Connect(true, ref identity);
        Assert.IsTrue(identity.Length > 0, "Echoed Identity should have non-zero length.");
        Assert.IsTrue(identity.StartsWith("Keithley"), "Identity should start with the manufacturer name.");
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
        TspDeviceTests.AssertDeviceShouldConfigureSource(ipv4Address, "CURR");
    }

    /// <summary>   (Unit Test Method) device should configure voltage source. </summary>
    /// <remarks>   2024-02-05. </remarks>
    [TestMethod]
    public void DeviceShouldConfigureVoltageSource()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldConfigureSource(ipv4Address, "VOLT");
    }

    /// <summary>   Assert device should measure resistance. </summary>
    /// <remarks>   2024-02-05. </remarks>
    /// <param name="ipv4Address">      The IPv4 address. </param>
    /// <param name="sourceFunction">   Source function. </param>
    public static void AssertDeviceShouldMeasureResistance(string ipv4Address, string sourceFunction)
    {
        using TspDevice session = new(ipv4Address);
        string identity = string.Empty;
        session.Connect(true, ref identity);
        Assert.IsTrue(identity.Length > 0, "Echoed Identity should have non-zero length.");
        Assert.IsTrue(identity.StartsWith("Keithley"), "Identity should start with the manufacturer name.");
        session.SourceFunction = sourceFunction;
        session.ConfigureConstantSource();
        session.MeasureResistance();
        Assert.IsTrue(session.Resistance.HasValue, "Resistance should be measured.");
        Assert.IsTrue(session.Resistance.Value > 1.9, "Resistance should be above 1.9 ohms");
        Assert.IsTrue(session.Resistance.Value < 2.1, "Resistance should be below 2.1 ohms");

        session.Disconnect();
    }

    [TestMethod]
    public void DeviceShouldMeasureConstantCurrentResistance()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldMeasureResistance(ipv4Address, "CURR");
    }

    [TestMethod]
    public void DeviceShouldMeasureConstantVoltageResistance()
    {
        string ipv4Address = IPAddress;
        TspDeviceTests.AssertDeviceShouldMeasureResistance(ipv4Address, "VOLT");
    }

}
