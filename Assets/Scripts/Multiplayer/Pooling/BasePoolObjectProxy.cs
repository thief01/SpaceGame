using Core.Pooling;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Pooling
{
    [RequireComponent(typeof(BasePoolObject))]
    public class BasePoolObjectProxy : MonoBehaviourPun
    {
        private PhotonView photonView;
        private BasePoolObject basePoolObject;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            basePoolObject = GetComponent<BasePoolObject>();
        }
    
    
        public virtual void Kill()
        {
            if (photonView.IsMine)
            {
                basePoolObject.Kill();
            }
            else
            {
                photonView.RPC("KillRPC", RpcTarget.Others);
            }
        }

        public void KillWithDelay(float delay)
        {
            if (photonView.IsMine)
            {
                basePoolObject.Kill();
            }
            else
            {
                photonView.RPC("KillWithDelayRPC", RpcTarget.Others, delay);
            }
        }

        [PunRPC]
        private void KillRPC()
        {
            if (photonView.IsMine)
            {
                basePoolObject.Kill();   
            }
        }

        [PunRPC]
        private void KillWithDelayRPC(float delay)
        {
            if (photonView.IsMine)
            {
                basePoolObject.KillWithDelay(delay);
            }
        }

    }
}
