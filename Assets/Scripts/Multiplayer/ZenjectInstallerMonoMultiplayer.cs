using System;
using UnityEngine;
using Zenject;


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
        
        Container.Bind<BasePool>().WithId("Bullets").FromInstance(new MultiplayerPool(bullet, 200));
        Container.Bind<BasePool>().WithId("Ships").FromInstance(new MultiplayerPool(ship));
        Container.Bind<BasePool>().WithId("Asteroids").FromInstance(new MultiplayerPool(asteroid, 50));
        Container.Bind<BasePool>().WithId("Explosions").FromInstance(new MultiplayerPool(explosion, 50));
    }
}