using Core.Pooling;
using Game.Classes;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Multiplayer
{
    public class ZenjectInstallerMonoMultiplayer : MonoInstaller<ZenjectInstallerMonoMultiplayer>
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject ship;
        [SerializeField] private GameObject asteroid;
        [SerializeField] private GameObject explosion;
    
        public override void InstallBindings()
        {
            Debug.Log("Installing bindings");
            Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());
        
            Container.Bind<BasePool>().WithId("Bullets").FromInstance(new MultiplayerPool(bullet, 20));
            Container.Bind<BasePool>().WithId("Ships").FromInstance(new MultiplayerPool(ship, 1));
            Container.Bind<BasePool>().WithId("Asteroids").FromInstance(new MultiplayerPool(asteroid, 30));
            Container.Bind<BasePool>().WithId("Explosions").FromInstance(new MultiplayerPool(explosion, 30));
        }
    }
}