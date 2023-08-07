using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CooldownControler))]
public class WeaponControler : MonoBehaviour
{
    public Transform Muzzle
    {
        get => muzzle;
        set
        {
            muzzle = value;
            if (weaponUserData != null)
                weaponUserData.Muzzle = muzzle;
        }
    }
    [SerializeField] private Transform muzzle;
    [SerializeField] private WeaponBehaviourBase weaponBehaviourBase;

    private WeaponUserData weaponUserData;

    private void Awake()
    {
        weaponUserData = new WeaponUserData();
        weaponUserData.Muzzle = muzzle;
        weaponUserData.Owner = transform;
        weaponUserData.CooldownControler = GetComponent<CooldownControler>();
    }


    public void Shoot(Vector3 target)
    {
        weaponUserData.Target = target;
        weaponBehaviourBase.Shoot(weaponUserData);
    }

    public void SetNewWeapon(WeaponBehaviourBase weaponBehaviourBase)
    {
        this.weaponBehaviourBase = weaponBehaviourBase;
    }
}
