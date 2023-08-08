using System;
using System.Collections.Generic;
using Core.Pooling;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Objects
{
    [RequireComponent(typeof(PhotonView))]
    public class AsteroidSpawner : MonoBehaviourPun
    {
        [Inject(Id = "Asteroids")] public BasePool asteroidsPool;
        [SerializeField] private int countOfAsteroidsPerPlayer = 5;
        [SerializeField] private float spawnZoneSize;

        private List<BasePoolObject> spawnedAsteroids = new List<BasePoolObject>();
        private PhotonView photonView;
    
        private int finalCountOfAsteroids
        {
            get
            {
                if(PhotonNetwork.IsConnected)
                    return countOfAsteroidsPerPlayer * PhotonNetwork.CountOfPlayers;
                return countOfAsteroidsPerPlayer * 4;
            }
        }

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            KillAsteroidsOutOfZone();
            UpdateAsteroids();
            SpawnAsteroids();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, spawnZoneSize);
        }

        private void UpdateAsteroids()
        {
            for (int i = 0; i < spawnedAsteroids.Count; i++)
            {
                if (!spawnedAsteroids[i].gameObject.activeSelf)
                {
                    spawnedAsteroids.RemoveAt(i);
                    i--;
                }
            }
        }

        private void SpawnAsteroids()
        {
            int asteroidsNeededToSpawn = finalCountOfAsteroids - spawnedAsteroids.Count;

            for (int i = 0; i < asteroidsNeededToSpawn; i++)
            {
                var asteroid = asteroidsPool.GetNewObject();
                if (asteroid != null)
                {
                    asteroid.transform.position = Quaternion.Euler(0, 0, Random.Range(0, 360)) *
                                                  new Vector3(Random.Range(-spawnZoneSize / 2, spawnZoneSize / 2), 0, 0);
                }
                else
                {
                    break;
                }
            }
        }

        private void KillAsteroidsOutOfZone()
        {
            for (int i = 0; i < spawnedAsteroids.Count; i++)
            {
                if (Vector3.Distance(transform.position, spawnedAsteroids[i].transform.position) >= spawnZoneSize)
                {
                    spawnedAsteroids[i].Kill();
                }
            }
        }
    }
}
