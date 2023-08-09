using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Pooling
{
    public class BasePool
    {
        public BasePoolObject[] BasePoolObjects => objects.ToArray();
        protected const string NOT_FOUND_BASE_POOL_OBJECT_WARNING =
            "Objects: {0} doesn't contain BasePoolObject it will be added but you should add it in prefab.";
    
        protected List<BasePoolObject> objects = new List<BasePoolObject>();

        protected GameObject baseGameObject;
        protected int countOfObjects;

        public BasePool(GameObject gameObject, int countOfObjects = 20)
        {
            //InitPool(gameObject, countOfObjects);
            baseGameObject = gameObject;
            this.countOfObjects = countOfObjects;
        }

        public void ForceActiveAllObjects(bool active)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].gameObject.SetActive(active);
            }
        }

    
        public virtual void InitPool(GameObject gameObject, int countOfObjects = 20)
        {
        
        }

        public virtual void InitPool()
        {
            InitPool(baseGameObject, countOfObjects);
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
}
