using System;
using apbd_02;
using apbd_02.exception;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apbd_02.Tests;

[TestClass]
[TestSubject(typeof(EmbeddedDevice))]
public class EmbeddedDeviceTest
{

    [TestMethod]
    public void TestArgumentExceptionForIP()
    {
        EmbeddedDevice ed;
        Assert.Throws<ArgumentException>(() =>
        {
            ed = new EmbeddedDevice("ED-1", "testname", true, "55.545.33.44", "MD Ltd.");
        });
    }

    [TestMethod]
    public void TestConnectionExceptionForNetworkName()
    {
        EmbeddedDevice ed;
        Assert.Throws<ConnectionException>(() =>
        {
            ed = new EmbeddedDevice("ED-1", "testname", true, "55.55.33.44", "HEHEHA");
        });
    }
}