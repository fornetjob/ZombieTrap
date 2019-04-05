using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FragmentTest
{
    [Test]
    public void IntTest()
    {
        Assert.AreEqual(1 / 10, 0);
        Assert.AreEqual(8 / 10, 0);
        Assert.AreEqual(9 / 10, 0);
        Assert.AreEqual(10 / 10, 1);
        Assert.AreEqual(19 / 10, 1);
        Assert.AreEqual(20 / 10, 2);
    }
}