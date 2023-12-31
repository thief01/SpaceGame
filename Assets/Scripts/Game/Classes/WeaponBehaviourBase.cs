using Core.Pooling;
using UnityEngine;
using Zenject;

namespace Game.Classes
{
    [CreateAssetMenu(menuName = "thief01/Weapons/Weapon base")]
    public class WeaponBehaviourBase : ScriptableObject
    {
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected float bulletSpeed;

        [Inject(Id = "Bullets")] public BasePool bulletsPool;
        
        public virtual void Shoot(WeaponUserData weaponUserData)
        {
            if (weaponUserData.CooldownControler.GetCooldown("WeaponBehaviourBase") > 0)
            {
                return;
            }
        
            SpawnBullet(weaponUserData);
        }

        protected void SpawnBullet(WeaponUserData weaponUserData)
        {
            var g = bulletsPool.GetNewObject();
            g.KillWithDelay(5);
            SetGameObjectData(g.gameObject, weaponUserData);
        }

        protected void SetGameObjectData(GameObject g, WeaponUserData weaponUserData)
        {
            g.transform.position = weaponUserData.Muzzle.position;
            g.transform.eulerAngles = GetUpDirection(weaponUserData);
            g.GetComponent<Rigidbody2D>().velocity = g.transform.up * bulletSpeed;
            weaponUserData.CooldownControler.AddNewCooldown(1 / attackSpeed, "WeaponBehaviourBase");
        }

        protected Vector3 GetUpDirection(WeaponUserData weaponUserData)
        {
            return new Vector3(0, 0,
                Mathf.Atan2(weaponUserData.Direction.y, weaponUserData.Direction.x) * Mathf.Rad2Deg - 90);
        }
    }
}
