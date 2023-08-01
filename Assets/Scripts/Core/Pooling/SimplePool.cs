using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : BasePool
{
    
    public SimplePool(GameObject gameObject, int countOfObjects = 20) : base(gameObject, countOfObjects)
    {
    }

    public override void InitPool(GameObject gameObject, int countOfObjects)
    {
        for (int i = 0; i < countOfObjects; i++)
        {
            GameObject g = GameObject.Instantiate(gameObject);
            g.SetActive(false);
            var basePoolObject = g.GetComponent<BasePoolObject>();
            if (basePoolObject == null)
            {
                Debug.LogWarning(string.Format(NOT_FOUND_BASE_POOL_OBJECT_WARNING, g.name));
                basePoolObject = g.AddComponent<BasePoolObject>();
            }

            basePoolObject.BasePool = this;
            objects.Add(basePoolObject);
        }
    }
    
    public override void ReturnObject(GameObject gameObject)
    {
        objects.Add(gameObject.GetComponent<BasePoolObject>());
        gameObject.SetActive(false);
    }

    public override void ReturnObject(BasePoolObject basePoolObject)
    {
        objects.Add(basePoolObject);
        basePoolObject.gameObject.SetActive(false);
    }
}
