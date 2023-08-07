using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageProvider
{
    void CalculateDamage(DamageInfo damageInfo);
}
