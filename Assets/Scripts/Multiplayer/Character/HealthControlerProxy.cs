using Game.Character;
using Game.Classes;
using Multiplayer.Classes;
using Multiplayer.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Character
{
    [RequireComponent(typeof(HealthControler))]
    public class HealthControlerProxy : MonoBehaviourPun, IDamageablePun, IKillablePun, IPunObservable
    {
        private HealthControler healthControler;
        private PhotonView photonView;
    
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            healthControler = GetComponent<HealthControler>();
        }

        public void DealDamage(DamageInfoPun damageInfo)
        {
            if (photonView.IsMine)
            {
                healthControler.DealDamage(damageInfo);
            }
            // else
            // {
            //     photonView.RPC("DealDamageRPC", RpcTarget.Others, damageInfo.damage, damageInfo.targetId, damageInfo.ownerId);
            // }
        }

        public void Kill(DamageInfoPun damageInfo)
        {
            if (photonView.IsMine)
            {
                healthControler.Kill(damageInfo);
            }
            // else
            // {
            //     photonView.RPC("KillRPC", RpcTarget.Others, damageInfo.damage, damageInfo.targetId, damageInfo.ownerId);
            // }
        }

        // [PunRPC]
        // private void KillRPC(float damage, int targetId, int ownerId)
        // {
        //     if (!photonView.IsMine)
        //         return;
        //     DamageInfo damageInfo = new DamageInfo()
        //     {
        //         damage = damage,
        //         damageTarget = PhotonNetwork.GetPhotonView(targetId).transform,
        //         damageOwner = PhotonNetwork.GetPhotonView(ownerId).transform,
        //     };
        //     healthController.Kill(damageInfo);
        // }
        //
        // [PunRPC]
        // private void DealDamageRPC(float damage, int targetId, int ownerId)
        // {
        //     if (!photonView.IsMine)
        //         return;
        //     DamageInfo damageInfo = new DamageInfo()
        //     {
        //         damage = damage,
        //         damageTarget = PhotonNetwork.GetPhotonView(targetId).transform,
        //         damageOwner = PhotonNetwork.GetPhotonView(ownerId).transform,
        //     };
        //     healthController.DealDamage(damageInfo);
        // }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(healthControler.CurrentHealth);
            }
            else
            {
                healthControler.SetHealth((int)stream.ReceiveNext());
            }
        }
    }
}
