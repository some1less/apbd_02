using apbd_02;
using apbd_02.exception;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apbd_02.Tests;

[TestClass]
[TestSubject(typeof(Smartwatch))]
public class SmartwatchTest
{

    [TestMethod]
    public void TestEmptyBatteryException()
    {
        Smartwatch sw = new Smartwatch("SW-1", "capi", false, 10);
        Assert.Throws<EmptyBatteryException>(() => sw.TurnMode());
    }
    
    [TestMethod]
    public void TestNoEmptyBatteryException()
    {
        Smartwatch sw = new Smartwatch("SW-1", "capi", false, 20);
        sw.TurnMode();
    }
    
    [TestMethod]
    public void TestReduceWhenTurnOn()
    {
        Smartwatch sw = new Smartwatch("SW-2", "capi", false, 20);
        sw.TurnMode();
        Assert.AreEqual(10, sw.BatteryLevel);
    }
}