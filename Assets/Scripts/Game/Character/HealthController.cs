using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class HealthController : MonoBehaviour, IDamageable
{
    public int CurrentHealth => currentHealth;

    public UnityEvent<DamageInfo>OnBeforeDie = new UnityEvent<DamageInfo>();
    public UnityEvent<DamageInfo>OnDie = new UnityEvent<DamageInfo>();
    public UnityEvent OnRespawn = new UnityEvent();
    public UnityEvent<DamageInfo>OnReceiveDamage = new UnityEvent<DamageInfo>();
    
    [SerializeField] private int baseHealth = 5;

    private int currentHealth;

    /*
     * Zenject doesn't innject durning the object is off.
     * So for now i have to force new Provider(); 
     */
    [Inject]
    public IDamageProvider damageProvider = new BaseDamageProvider();
    
    
    private void Awake()
    {
        Respawn();
    }
    
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
}
