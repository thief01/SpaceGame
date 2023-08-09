using System.Collections;
using Game.Character;
using Game.Classes;
using Game.Interfaces;
using Game.ZenjectInstallers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.Game.Character
{
    [TestFixture]
    public class HealthControllerTest : ZenjectUnitTestFixture
    {
        private HealthControler healthControler;
        private GameObject gameObject;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ZenjectInstaller.Install(Container);
        
            gameObject = new GameObject("Health controller");
            healthControler = Container.InstantiateComponent<HealthControler>(gameObject);
        }

        [Test]
        public void DamageProvider()
        {
            Assert.NotNull(Container.Resolve<IDamageProvider>());
        }

        [UnityTest]
        public IEnumerator HealthControllerTestWithEnumeratorPasses()
        {
            yield return null;
            int startHealth = healthControler.CurrentHealth;
            healthControler.DealDamage(new DamageInfo() { damage = 5 });
            Assert.AreEqual(startHealth - 5, healthControler.CurrentHealth);
        }

        [TearDown]
        public void TearDown()
        {
            Container.UnbindAll();
        }
    }
}
