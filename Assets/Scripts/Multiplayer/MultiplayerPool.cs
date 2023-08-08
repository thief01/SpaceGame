using Core.Pooling;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerPool : BasePool
    {
        public MultiplayerPool(GameObject gameObject, int countOfObjects = 20) : base(gameObject, countOfObjects)
        {
        
        }

        public override void InitPool(GameObject gameObject, int countOfObjects)
        {
            for (int i = 0; i < countOfObjects; i++)
            {
                GameObject g = PhotonNetwork.Instantiate("Photon/Pools/"+gameObject.name, Vector3.zero, Quaternion.identity);
                g.SetActive(false);
                var basePoolObject = g.GetComponent<BasePoolObject>();
                if (basePoolObject == null)
                {
                    Debug.LogWarning(string.Format(NOT_FOUND_BASE_POOL_OBJECT_WARNING, g.name));
                    basePoolObject = g.AddComponent<BasePoolObject>();
                }

                basePoolObject.BasePool = this;
                objects.Add(basePoolObject);
            }
        }
    }
}
