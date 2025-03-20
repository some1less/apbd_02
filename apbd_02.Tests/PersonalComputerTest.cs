using apbd_02;
using apbd_02.exception;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apbd_02.Tests;

[TestClass]
[TestSubject(typeof(PersonalComputer))]
public class PersonalComputerTest
{

    [TestMethod]
    public void TestEmptySystemException()
    {
        PersonalComputer pc;
        Assert.Throws<EmptySystemException>(() =>
        {
            pc = new PersonalComputer("P-1", "capi", true, null);
        });
    }
    
    [TestMethod]
    public void TestNoEmptySystemException()
    {
        PersonalComputer pc;
        pc = new PersonalComputer("P-1", "capi", true, "Helloo");
    }
}