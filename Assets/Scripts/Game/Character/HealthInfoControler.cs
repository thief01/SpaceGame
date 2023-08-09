using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

public class HealthInfoControler : MonoBehaviour
{
    private const float ANIMATION_DELAy = 0.3f;
    private const float FIRST_ANIMATION_SPEED = 1.5f;
    private const float SECOND_ANIMATION_SPEED = 0.5f;
    
    [SerializeField] private HealthController healthController;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private Image animatedHealthBar;
    
    private void Awake()
    {
        healthController.OnReceiveDamage.AddListener(OnReceivedDamage);
    }

    private void OnEnable()
    {
        OnReceivedDamage(null);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnReceivedDamage(DamageInfo damageInfo)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateBar(healthBar, 0, FIRST_ANIMATION_SPEED));
        StartCoroutine(AnimateBar(animatedHealthBar, ANIMATION_DELAy, SECOND_ANIMATION_SPEED));
    }

    private IEnumerator AnimateBar(Image bar, float delay, float animationSpeed)
    {
        yield return new WaitForSeconds(delay);

        float delta = 0;
        float startValue = bar.fillAmount;

        while (delta < 1)
        {
            bar.fillAmount = Mathf.Lerp(startValue, healthController.PercentHealth, delta);
            delta += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }
}
