using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Multiplayer.Character
{
    [RequireComponent(typeof(TextMeshPro))]
    public class NicknameUpdater : MonoBehaviour, IPunObservable
    {
        [SerializeField] private PhotonView photonView;
        private TextMeshPro nickname;

        private void Awake()
        {
            nickname = GetComponent<TextMeshPro>();
            if (photonView.IsMine)
            {
                nickname.text = PhotonNetwork.NickName;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (photonView.IsMine)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(PhotonNetwork.NickName);
                }
            }

            if (stream.IsReading)
            {
                nickname.text = (string)stream.ReceiveNext();
            }
        }
    }
}