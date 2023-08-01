using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonSpawner : MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.Instantiate("PlayerPUN", Vector3.zero, Quaternion.identity);
    }


}
