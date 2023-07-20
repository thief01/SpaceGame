using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class HealthController : MonoBehaviour, IDamageable
{
    public int CurrentHealth => currentHealth;
    
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnRespawn = new UnityEvent();
    public UnityEvent<DamageInfo>OnReceiveDamage = new UnityEvent<DamageInfo>();
    
    [SerializeField] private int baseHealth = 5;

    private int currentHealth;

    [Inject]
    public IDamageProvider damageProvider;
    
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
    }

    public void Kill()
    {
        OnDie.Invoke();
    }
}
