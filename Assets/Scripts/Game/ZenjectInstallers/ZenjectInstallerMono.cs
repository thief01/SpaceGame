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
        [SerializeField] protected GameObject bullet;
        [SerializeField] protected GameObject ship;
        [SerializeField] protected GameObject asteroid;
        [SerializeField] protected GameObject explosion;

        [SerializeField] protected GameObject cameraManager;

        [SerializeField] protected WeaponBehaviourBase weaponBehaviourBase;

        [SerializeField] protected List<ScriptableObject> scriptableObjectsToInject;

        protected BasePool bullets;
        protected BasePool ships;
        protected BasePool asteroids;
        protected BasePool explosions;

        protected List<BasePool> allPools = new List<BasePool>();

        public override void InstallBindings()
        {
            Debug.Log("Installing bindings");
            Container.Bind<IDamageProvider>().To<IDamageProvider>().FromInstance(new BaseDamageProvider());
            Container.Bind<WeaponBehaviourBase>().FromInstance(weaponBehaviourBase);
            BindPools();
            InjectScriptables();
            InjectToPools();
        
            InitPlayer();
        }

        protected virtual void BindPools()
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

        protected void BindNewPool(BasePool simplePool, string poolId)
        {
            Container.Bind<BasePool>().WithId(poolId).FromInstance(simplePool);
            simplePool.InitPool();
            allPools.Add(simplePool);
        }

        protected void InitPlayer()
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

        protected void InjectScriptables()
        {
            for (int i = 0; i < scriptableObjectsToInject.Count; i++)
            {
                Container.QueueForInject(scriptableObjectsToInject[i]);
            }
        }

        protected void InjectToPools()
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