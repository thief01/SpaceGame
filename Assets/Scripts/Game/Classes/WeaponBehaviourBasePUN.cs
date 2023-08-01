using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[CreateAssetMenu(menuName = "thief01/PUN/Weapon base" )]
public class WeaponBehaviourBasePUN : WeaponBehaviourBase
{
    [SerializeField] private string prefabName;


    public override void Shoot(WeaponUserData weaponUserData)
    {
        if (weaponUserData.CooldownControler.GetCooldown("WeaponBehaviourBase") > 0)
        {
            return;
        }

        GameObject g = PhotonNetwork.Instantiate(prefabName, weaponUserData.Muzzle.position, Quaternion.Euler(GetUpDirection(weaponUserData)));
        g.GetComponent<Rigidbody2D>().velocity = g.transform.up * bulletSpeed;
        weaponUserData.CooldownControler.AddNewCooldown(1 / attackSpeed, "WeaponBehaviourBase");
    }
}
