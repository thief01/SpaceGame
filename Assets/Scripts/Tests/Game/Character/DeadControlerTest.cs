using System.Collections;
using Game.Character;
using Game.Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.Game.Character
{
    public class DeadControlerTest : ZenjectUnitTestFixture
    {
        private const float WAIT_TILL_ANIMATION_END = 2;
        private GameObject loadedAsteroid;
    
        [SetUp]
        public override void Setup()
        {
            loadedAsteroid = Resources.Load<GameObject>("Tests/AsteroidForTest");
        
        }

        [UnityTest]
        public IEnumerator FX()
        {
            var spawnedAsteroid = GameObject.Instantiate(loadedAsteroid).GetComponent<HealthControler>();
            spawnedAsteroid.Kill(new DamageInfo(){ });
            yield return null;
            Assert.IsNotNull(GameObject.FindGameObjectWithTag("Explosion"));
        }
    
        [UnityTest]
        public IEnumerator KillAsteroid()
        {
            var spawnedAsteroid = GameObject.Instantiate(loadedAsteroid).GetComponent<HealthControler>();
            spawnedAsteroid.Kill(new DamageInfo(){ });
            yield return null;
            Assert.IsNull(GameObject.FindGameObjectWithTag("Asteroid"));
        }
    
        [UnityTest]
        public IEnumerator KillFX()
        {
            var spawnedAsteroid = GameObject.Instantiate(loadedAsteroid).GetComponent<HealthControler>();
            spawnedAsteroid.Kill(new DamageInfo(){ });
            yield return new WaitForSeconds(WAIT_TILL_ANIMATION_END);
            Assert.IsNull(GameObject.FindGameObjectWithTag("Explosion"));
        }
    }
}
