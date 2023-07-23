using UnityEngine;
using Zenject;


public class ZenjectInstaller : Installer<ZenjectInstaller>
{
    public override void InstallBindings()
    {
        Debug.Log("Installing bindings");
        Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());
        Container.Bind<PlayerData>().AsSingle();
    }
}