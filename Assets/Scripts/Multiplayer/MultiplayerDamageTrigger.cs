using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using Game.Objects;
using Photon.Pun;
using UnityEngine;

public class MultiplayerDamageTrigger : DamageTriggerBase
{
    private PhotonView photonView;

    protected override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        var damageable = col.gameObject.GetComponent<IDamageablePun>();
        if (damageable == null)
            return;
        var targetPhotonView = col.gameObject.GetComponent<PhotonView>();

        DamageInfoPun damageInfoPun = new DamageInfoPun()
        {
            damage = damageInfo.damage,
            ownerId = photonView.ViewID,
            targetId = targetPhotonView.ViewID
        };
        damageable.DealDamage(damageInfoPun);
        OnDie.Invoke();

    }
}
