using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ZenjectInstallerMono : MonoInstaller<ZenjectInstallerMono>
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject asteroid;
    [SerializeField] private GameObject explosion;

    [SerializeField] private List<ScriptableObject> scriptableObjectsToInject;

    public override void InstallBindings()
    {
        Debug.Log("Installing bindings");
        Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());

        Container.Bind<BasePool>().WithId("Bullets").FromInstance(new SimplePool(bullet, 200));
        Container.Bind<BasePool>().WithId("Ships").FromInstance(new SimplePool(ship, 1));
        Container.Bind<BasePool>().WithId("Asteroids").FromInstance(new SimplePool(asteroid, 25));
        Container.Bind<BasePool>().WithId("Explosions").FromInstance(new SimplePool(explosion, 25));
        InjectScriptables();
    }

    private void InjectScriptables()
    {
        for (int i = 0; i < scriptableObjectsToInject.Count; i++)
        {
            Container.QueueForInject(scriptableObjectsToInject[i]);
        }
    }
}