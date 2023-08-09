using Game.Classes;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Character
{
    public class HealthControler : MonoBehaviour, IDamageable, IKillable
    {
        public int CurrentHealth => currentHealth;

        public float PercentHealth => (float)currentHealth / (float)baseHealth;

        public UnityEvent<DamageInfo>OnBeforeDie = new UnityEvent<DamageInfo>();
        public UnityEvent<DamageInfo>OnDie = new UnityEvent<DamageInfo>();
        public UnityEvent OnRespawn = new UnityEvent();
        public UnityEvent<DamageInfo>OnReceiveDamage = new UnityEvent<DamageInfo>();
    
        [SerializeField] private int baseHealth = 5;

        private int currentHealth;
    
        [Inject] public IDamageProvider damageProvider;
    
    
        private void Awake()
        {
            Respawn();
        }

        private float time;

        public void Respawn()
        {
            currentHealth = baseHealth;
        
            OnRespawn.Invoke();
        }

        public void DealDamage(DamageInfo damageInfo)
        {
            damageInfo.damageTarget = transform;
            damageProvider.CalculateDamage(damageInfo);
            currentHealth -= (int)damageInfo.calculatedDamage;
            OnReceiveDamage.Invoke(damageInfo);

            if (currentHealth <= 0)
            {
                OnBeforeDie.Invoke(damageInfo);
                Kill(damageInfo);
            }
        }

        public void Kill(DamageInfo damageInfo)
        {
            OnDie.Invoke(damageInfo);
        }

        public void SetHealth(int health)
        {
            currentHealth = health;
            OnReceiveDamage.Invoke(null);
        }
    }
}
