using System;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class PhotonGameObject : MonoBehaviourPun, IPunObservable
    {
        private PhotonView photonView;
        private bool active = false;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }
        
        private void OnEnable()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SwitchEnableObject", RpcTarget.OthersBuffered, true);
                active = true;
            }
        }

        private void OnDisable()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SwitchEnableObject", RpcTarget.OthersBuffered, false);
                active = false;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(gameObject.activeSelf);
            }
            else
            {
                var active = (bool)stream.ReceiveNext();
                gameObject.SetActive(active);
            
            }
        }

        [PunRPC]
        public void SwitchEnableObject(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
