using System;
using System.Collections;
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

    private SimplePool bullets;
    private SimplePool ships;
    private SimplePool asteroids;
    private SimplePool explosions;

    private List<SimplePool> allPools = new List<SimplePool>();

    public override void InstallBindings()
    {
        Debug.Log("Installing bindings");
        Container.Bind<IDamageProvider>().To<IDamageProvider>().FromInstance(new BaseDamageProvider());
        BindPools();
        InjectScriptables();
        InjectToPools();
        
        InitPlayer();
    }

    private void BindPools()
    {
        bullets = new SimplePool(bullet, 10);
        ships = new SimplePool(ship, 1);
        asteroids = new SimplePool(asteroid, 10);
        explosions = new SimplePool(explosion, 10);
        
        Container.Bind<BasePool>().WithId("Bullets").FromInstance(bullets);
        Container.Bind<BasePool>().WithId("Ships").FromInstance(ships);
        Container.Bind<BasePool>().WithId("Asteroids").FromInstance(asteroids);
        Container.Bind<BasePool>().WithId("Explosions").FromInstance(explosions);
        
        
        bullets.InitPool();
        ships.InitPool();
        asteroids.InitPool();
        explosions.InitPool();

        allPools.Add(bullets);
        allPools.Add(ships);
        allPools.Add(asteroids);
        allPools.Add(explosions);
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

    private void InjectToPools()
    {
        foreach (var pool in allPools)
        {
            for (int i = 0; i < pool.BasePoolObjects.Length; i++)
            {
                Container.InjectGameObject(pool.BasePoolObjects[i].gameObject);
            }
        }
    }
}