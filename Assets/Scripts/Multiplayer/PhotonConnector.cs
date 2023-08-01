using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();

        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the master");
        PhotonNetwork.JoinLobby();
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected to the serwer");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to the lobby");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        roomOptions.PlayerTtl = 0;
        roomOptions.IsVisible = true;

        if(!PhotonNetwork.JoinOrCreateRoom("Base room", roomOptions, TypedLobby.Default))
        {
            Debug.Log("Error connecting to room");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to room");
        Debug.Log(PhotonNetwork.CurrentRoom);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
        Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
    }
}
