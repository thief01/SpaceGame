using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthControllerTest
{
    private HealthController healthController;
    
    [SetUp]
    public void SetUp()
    {
        healthController = new GameObject("Health controller").AddComponent<HealthController>();
    }

    [UnityTest]
    public IEnumerator HealthControllerTestWithEnumeratorPasses()
    {
        yield return null;
        yield return null;
        int startHealth = healthController.CurrentHealth;
        healthController.DealDamage(new DamageInfo() { damage = 5 });
        Assert.AreEqual(startHealth - 5, healthController.CurrentHealth);
    }
}
