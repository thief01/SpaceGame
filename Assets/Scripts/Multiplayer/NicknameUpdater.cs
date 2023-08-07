using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class NicknameUpdater : MonoBehaviour
{
    private TextMeshPro nickname;

    private void Awake()
    {
        nickname = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        nickname.text = PhotonNetwork.NickName;
    }
}
