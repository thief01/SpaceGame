using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class WeaponControlerTest : ZenjectUnitTestFixture
{
    private const float VELOCITY_TOLERANCE = 0.05f;
    
    private WeaponControler weaponControler;
    private CooldownControler cooldownControler;
    private WeaponBehaviourBase weaponBehaviourBase;

    [SetUp]
    public override void Setup()
    {
        weaponBehaviourBase = Resources.Load<WeaponBehaviourBase>("Test/TestWeapon");
        GameObject g = new GameObject("Cooldown and weapon test");
        weaponControler = g.AddComponent<WeaponControler>();
        weaponControler.SetNewWeapon(weaponBehaviourBase);
        cooldownControler = g.AddComponent<CooldownControler>();
        GameObject muzzle = new GameObject("Muzzle");
        muzzle.transform.parent = g.transform;
        muzzle.transform.localPosition = Vector3.zero;
        weaponControler.Muzzle = muzzle.transform;
    }

    [Test]
    public void LoadedWeaponBehaviour()
    {
        Assert.IsNotNull(weaponBehaviourBase);
    }
    
    [Test]
    public void WeaponController()
    {
        Assert.IsNotNull(weaponControler);
    }

    [Test]
    public void CooldownControler()
    {
        Assert.IsNotNull(cooldownControler);
    }

    [Test]
    public void MuzzleTest()
    {
        Assert.IsNotNull(weaponControler.Muzzle);
    }

    [UnityTest]
    public IEnumerator SpawnBullet()
    {
        yield return null;
        weaponControler.Shoot(new Vector3(1,1));

        var obj = GameObject.FindGameObjectWithTag("Bullet");
        Assert.IsNotNull(obj);
    }

    [UnityTest]
    public IEnumerator BulletDirection()
    {
        yield return null;
        
        weaponControler.Shoot(new Vector3(1,1));

        var obj = GameObject.FindGameObjectWithTag("Bullet");
        var rb = obj.GetComponent<Rigidbody2D>();
        Assert.LessOrEqual(Vector3.Distance(new Vector3(1,1).normalized, rb.velocity.normalized), VELOCITY_TOLERANCE);
    }
}
