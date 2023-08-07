using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class CooldownControlerTest : ZenjectUnitTestFixture
{
    private CooldownControler cooldownControler;

    [SetUp]
    public override void Setup()
    {
        GameObject g = new GameObject("Cooldown test");
        cooldownControler = g.AddComponent<CooldownControler>();
    }

    [Test]
    public void TestCooldownController()
    {
        Assert.IsNotNull(cooldownControler);
    }

    [UnityTest]
    public IEnumerator TestAddCooldown()
    {
        yield return null;
        cooldownControler.AddNewCooldown(10, "CooldownTest");
        Assert.Greater(cooldownControler.GetCooldown("CooldownTest"), 0);
    }

    [UnityTest]
    public IEnumerator TestCooldownTime()
    {
        cooldownControler.AddNewCooldown(0.5f, "CooldownTest");
        yield return new WaitForSeconds(0.5f);
        Assert.LessOrEqual(cooldownControler.GetCooldown("CooldownTest"), 0);
    }
}
