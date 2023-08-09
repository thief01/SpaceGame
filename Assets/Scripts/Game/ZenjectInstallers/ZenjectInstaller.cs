using Game.Classes;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.ZenjectInstallers
{
    public class ZenjectInstaller : Installer<ZenjectInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("Installing bindings");
            Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());
            Container.Bind<PlayerData>().AsSingle();
        }
    }
}