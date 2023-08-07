using System.Collections.Generic;
using Core.Camera;
using Core.Pooling;
using Game.Character;
using Game.Classes;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.ZenjectInstallers
{
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
            
            BindNewPool(bullets, "Bullets");
            BindNewPool(ships, "Ships");
            BindNewPool(asteroids, "Asteroids");
            BindNewPool(explosions, "Explosions");
        }

        private void BindNewPool(SimplePool simplePool, string poolId)
        {
            Container.Bind<SimplePool>().WithId(poolId).FromInstance(simplePool);
            simplePool.InitPool();
            allPools.Add(simplePool);
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
}