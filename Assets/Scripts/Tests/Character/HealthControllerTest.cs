using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

[TestFixture]
public class HealthControllerTest : ZenjectUnitTestFixture
{
    private HealthController healthController;
    private GameObject gameObject;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        ZenjectInstaller.Install(Container);
        
        gameObject = new GameObject("Health controller");//.AddComponent<HealthController>();
        healthController = Container.InstantiateComponent<HealthController>(gameObject);
    }

    [Test]
    public void DamageProviderTest()
    {
        Assert.NotNull(Container.Resolve<IDamageProvider>());
    }

    [UnityTest]
    public IEnumerator HealthControllerTestWithEnumeratorPasses()
    {
        yield return null;
        int startHealth = healthController.CurrentHealth;
        healthController.DealDamage(new DamageInfo() { damage = 5 });
        Assert.AreEqual(startHealth - 5, healthController.CurrentHealth);
    }

    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}
