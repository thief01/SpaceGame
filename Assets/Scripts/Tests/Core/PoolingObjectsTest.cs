using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PoolingObjectsTest 
{
    [UnityTest]
    public IEnumerator PoolGet()
    {
        SimplePool simplePool = new SimplePool(Resources.Load<GameObject>("Tests/AsteroidForTest"), 10);
        yield return null;
        var obj = simplePool.GetNewObject();
        yield return null;
        Assert.IsNotNull(obj);
    }
    
    [UnityTest]
    public IEnumerator PoolReturn()
    {
        SimplePool simplePool = new SimplePool(Resources.Load<GameObject>("Tests/AsteroidForTest"), 10);
        yield return null;
        var obj = simplePool.GetNewObject();
        obj.Kill();
        Assert.IsFalse(obj.gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator PoolDelayReturn()
    {
        SimplePool simplePool = new SimplePool(Resources.Load<GameObject>("Tests/AsteroidForTest"), 10);
        yield return null;
        var obj = simplePool.GetNewObject();
        obj.KillWithDelay(1);
        yield return new WaitForSeconds(1);
        Assert.IsFalse(obj.gameObject.activeSelf);
    }
}
