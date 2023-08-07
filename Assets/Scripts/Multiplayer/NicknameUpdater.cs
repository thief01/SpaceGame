using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Multiplayer
{
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
}
