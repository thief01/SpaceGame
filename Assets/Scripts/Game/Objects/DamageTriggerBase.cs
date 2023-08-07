using Game.Classes;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Objects
{
    public class DamageTriggerBase : MonoBehaviour
    {
        public UnityEvent OnDie = new UnityEvent();
        [SerializeField] private float damage;
        private DamageInfo damageInfo = new DamageInfo();

        private void Awake()
        {
            damageInfo.damageOwner = transform;
            damageInfo.damage = damage;

        }

        public void SetDamage(int damage)
        {
            damageInfo.damage = damage;
        }
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            var damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable == null)
                return;
            damageable.DealDamage(damageInfo);
            OnDie.Invoke();
        }
    }
}
