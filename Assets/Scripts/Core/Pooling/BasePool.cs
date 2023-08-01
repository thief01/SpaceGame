using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePool
{
    protected const string NOT_FOUND_BASE_POOL_OBJECT_WARNING =
        "Objects: {0} doesn't contain BasePoolObject it will be added but you should add it in prefab.";
    
    protected List<BasePoolObject> objects = new List<BasePoolObject>();

    public BasePool(GameObject gameObject, int countOfObjects = 20)
    {
        InitPool(gameObject, countOfObjects);
    }

    public virtual void InitPool(GameObject gameObject, int countOfObjects = 20)
    {
        
    }

    public virtual BasePoolObject GetNewObject()
    {
        if (objects.Count == 0)
            return null;

        var obj = objects[0];
        obj.gameObject.SetActive(true);
        objects.RemoveAt(0);
        return obj;
    }

    [Obsolete("I think you should use ReturnObject(BasePoolObject) instadead this.")]
    public virtual void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        var basePoolObject = gameObject.GetComponent<BasePoolObject>();
        if (objects.Find(ctg => ctg == basePoolObject) != null)
        {
            return;
        }
        objects.Add(basePoolObject);
    }

    public virtual void ReturnObject(BasePoolObject basePoolObject)
    {
        basePoolObject.gameObject.SetActive(false);
        if (objects.Find(ctg => ctg == basePoolObject) != null)
        {
            return;
        }
        objects.Add(basePoolObject);
    }
}
