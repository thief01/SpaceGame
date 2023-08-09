using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Photon.Pun;
using UnityEngine;


[RequireComponent(typeof(DeadFxControler))]
public class DeadFxControlerProxy : MonoBehaviourPun
{
    private PhotonView photonView;
    private DeadFxControler deadFxControler;

    private void Awake()
    {
        deadFxControler = GetComponent<DeadFxControler>();
        photonView = GetComponent<PhotonView>();
    }

    public void OnDie()
    {
        if (photonView.IsMine)
        {
            deadFxControler.OnDie();
        }
        else
        {
            photonView.RPC("OnDieRPC", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void OnDieRPC()
    {
        if (photonView.IsMine)
        {
            deadFxControler.OnDie();
        }
    }
}
