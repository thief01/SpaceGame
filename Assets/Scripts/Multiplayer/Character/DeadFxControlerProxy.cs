using Game.Character;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Character
{
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
        }
    }
}
