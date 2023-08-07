using Photon.Pun;

namespace Multiplayer
{
    public class PhotonGameObject : MonoBehaviourPun, IPunObservable
    {
        private PhotonView photonView;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SwitchEnableObject", RpcTarget.OthersBuffered, true);
            }
        }

        private void OnDisable()
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SwitchEnableObject", RpcTarget.OthersBuffered, false);
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
