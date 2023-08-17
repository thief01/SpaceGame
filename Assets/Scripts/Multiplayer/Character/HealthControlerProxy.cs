using Game.Character;
using Game.Classes;
using Multiplayer.Classes;
using Multiplayer.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Character
{
    [RequireComponent(typeof(HealthControler))]
    public class HealthControlerProxy : MonoBehaviour, IDamageablePun, IKillablePun, IPunObservable
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
        }

        public void Kill(DamageInfoPun damageInfo)
        {
            if (photonView.IsMine)
            {
                healthControler.Kill(damageInfo);
            }
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(healthControler.CurrentHealth);
            }
            
            if (stream.IsReading)
            {
                int newHealth = (int)stream.ReceiveNext();
                healthControler.SetHealth(newHealth);
            }
        }
    }
}
