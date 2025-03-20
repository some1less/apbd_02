using System.Collections.Generic;
using apbd_02;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apbd_02.Tests;

[TestClass]
[TestSubject(typeof(DeviceManager))]
public class DeviceManagerTest
{
    
    private string filepath = "inputtest.txt";

    [TestMethod]
    public void TurnOn()
    {
       
        DeviceManager dm = new DeviceManager(filepath);
        
        PersonalComputer pc = new PersonalComputer("P-1","capibara",true,"lmao");
        
        dm.AddDevice(pc);
        dm.TurnOnDevice(pc.Id);
        Assert.IsTrue(pc.IsTurnedOn);
        
        
    }

    [TestMethod]
    public void TurnOff()
    {
        DeviceManager dm = new DeviceManager(filepath);
        
        EmbeddedDevice ed = new EmbeddedDevice("ED-1","capibara",true,"55.55.33.44","MD Ltd.");
        
        dm.AddDevice(ed);
        dm.TurnOffDevice(ed.Id);
        Assert.IsFalse(ed.IsTurnedOn);
        
    }
    
}