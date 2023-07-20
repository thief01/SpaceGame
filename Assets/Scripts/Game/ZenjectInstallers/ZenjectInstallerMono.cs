using System;
using UnityEngine;
using Zenject;


public class ZenjectInstallerMono : MonoInstaller<ZenjectInstallerMono>
{
    public override void InstallBindings()
    {
        Debug.Log("Installing bindings");
        Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());
    }
}