using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageProvider : IDamageProvider
{
    public void CalculateDamage(DamageInfo damageInfo)
    {
        damageInfo.calculatedDamage = damageInfo.damage;
    }
}
