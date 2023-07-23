using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "thief01/Weapons/Weapon base")]
public class WeaponBehaviourBase : ScriptableObject
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    
    public virtual void Shoot(WeaponUserData weaponUserData)
    {
        if (weaponUserData.CooldownControler.GetCooldown("WeaponBehaviourBase") > 0)
        {
            return;
        }
        GameObject g = Instantiate(bulletPrefab);
        g.transform.position = weaponUserData.Muzzle.position;
        g.transform.eulerAngles = new Vector3(0, 0,
            Mathf.Atan2(weaponUserData.Direction.y, weaponUserData.Direction.x) * Mathf.Rad2Deg - 90);
        g.GetComponent<Rigidbody2D>().velocity = g.transform.up * bulletSpeed;
        weaponUserData.CooldownControler.AddNewCooldown(1 / attackSpeed, "WeaponBehaviourBase");
    }
}
