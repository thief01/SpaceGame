using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadControler : MonoBehaviour
{
    [SerializeField] private GameObject spawnFX;
    [SerializeField] private float fxAliveTime;


    public void OnDie()
    {
        if (spawnFX != null)
        {
            GameObject g = Instantiate(spawnFX);
            g.transform.position = transform.position;
            Destroy(g, fxAliveTime);
        }       

        Destroy(gameObject);
    }

}
