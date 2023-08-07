using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class DeadFxControler : MonoBehaviour
{
    [SerializeField] private float fxAliveTime;

    [Inject(Id = "Explosions")] public BasePool fxPool;

    public void OnDie()
    {
        BasePoolObject g = fxPool.GetNewObject();
        g.transform.position = transform.position;
        g.KillWithDelay(fxAliveTime);
    }

}
