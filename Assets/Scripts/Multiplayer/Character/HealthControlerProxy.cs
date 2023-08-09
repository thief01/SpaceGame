using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Classes;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class HealthControlerProxy : MonoBehaviourPun, IDamageablePun, IKillablePun, IPunObservable
{
    private HealthController healthController;
    private PhotonView photonView;
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        healthController = GetComponent<HealthController>();
    }

    public void DealDamage(DamageInfoPun damageInfo)
    {
        if (photonView.IsMine)
        {
            healthController.DealDamage(damageInfo);
        }
        else
        {
            photonView.RPC("DealDamageRPC", RpcTarget.Others, damageInfo.damage, damageInfo.targetId, damageInfo.ownerId);
        }
    }

    public void Kill(DamageInfoPun damageInfo)
    {
        if (photonView.IsMine)
        {
            healthController.Kill(damageInfo);
        }
        else
        {
            photonView.RPC("KillRPC", RpcTarget.Others, damageInfo.damage, damageInfo.targetId, damageInfo.ownerId);
        }
    }

    [PunRPC]
    private void KillRPC(float damage, int targetId, int ownerId)
    {
        if (!photonView.IsMine)
            return;
        DamageInfo damageInfo = new DamageInfo()
        {
            damage = damage,
            damageTarget = PhotonNetwork.GetPhotonView(targetId).transform,
            damageOwner = PhotonNetwork.GetPhotonView(ownerId).transform,
        };
        healthController.Kill(damageInfo);
    }

    [PunRPC]
    private void DealDamageRPC(float damage, int targetId, int ownerId)
    {
        if (!photonView.IsMine)
            return;
        DamageInfo damageInfo = new DamageInfo()
        {
            damage = damage,
            damageTarget = PhotonNetwork.GetPhotonView(targetId).transform,
            damageOwner = PhotonNetwork.GetPhotonView(ownerId).transform,
        };
        healthController.DealDamage(damageInfo);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(healthController.CurrentHealth);
        }
        else
        {
            healthController.SetHealth((int)stream.ReceiveNext());
        }
    }
}
