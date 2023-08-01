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

    [SerializeField] private GameObject cameraManager;

    [SerializeField] private List<ScriptableObject> scriptableObjectsToInject;

    private SimplePool ships;
    
    public override void InstallBindings()
    {
        Debug.Log("Installing bindings");
        Container.Bind<IDamageProvider>().To<BaseDamageProvider>().FromInstance(new BaseDamageProvider());

        ships = new SimplePool(ship, 1);
        Container.Bind<BasePool>().WithId("Bullets").FromInstance(new SimplePool(bullet, 200));
        Container.Bind<BasePool>().WithId("Ships").FromInstance(ships);
        Container.Bind<BasePool>().WithId("Asteroids").FromInstance(new SimplePool(asteroid, 25));
        Container.Bind<BasePool>().WithId("Explosions").FromInstance(new SimplePool(explosion, 25));
        InitPlayer();
        InjectScriptables();
    }

    private void InitPlayer()
    {
        var player = ships.GetNewObject().GetComponent<MovementControler>();
        player.transform.position = Vector3.zero;
        player.transform.eulerAngles = Vector3.zero;
        Container.Bind<MovementControler>().To<MovementControler>().FromInstance(player.GetComponent<MovementControler>())
            .AsSingle();
        
        CameraManager cameraManager = Instantiate(this.cameraManager).GetComponent<CameraManager>();
        cameraManager.CinemachineVirtualCamera.Follow = player.transform;
        cameraManager.CinemachineVirtualCamera.LookAt = player.transform;
        
        Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle();
    }

    private void InjectScriptables()
    {
        for (int i = 0; i < scriptableObjectsToInject.Count; i++)
        {
            Container.QueueForInject(scriptableObjectsToInject[i]);
        }
    }
}