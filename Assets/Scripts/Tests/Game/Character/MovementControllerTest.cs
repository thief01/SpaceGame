using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

[TestFixture]
public class MovementControllerTest : ZenjectUnitTestFixture
{
    private const float MAX_TIME_OUT = 15;
    private const float ACCELERATION_TOLLERANCY_TIME_MULTIPLITER = 2;
    
    private MovementControler movementControler;
    private GameObject gameObject;
    private Rigidbody2D rigidbody2D;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        ZenjectInstaller.Install(Container);
        
        gameObject = new GameObject("Movement controller");
        movementControler = Container.InstantiateComponent<MovementControler>(gameObject);
        rigidbody2D = movementControler.GetComponent<Rigidbody2D>();
    }

    [Test]
    public void Rigidbody()
    {
        Assert.IsNotNull(rigidbody2D);
    }

    [Test]
    public void MovementController()
    {
        Assert.IsNotNull(movementControler);
    }

    [UnityTest]
    public IEnumerator Acceleration()
    {
        yield return null;
        movementControler.Accelerate(1);
        Assert.Greater(rigidbody2D.velocity.magnitude, 0);
    }
    
    [UnityTest]
    public IEnumerator AccelerationOneSecond()
    {
        yield return Accelerate();
        Assert.GreaterOrEqual(rigidbody2D.velocity.magnitude, movementControler.MovingAcceleration);
    }

    [UnityTest]
    public IEnumerator EmergencyStop()
    {
        yield return Accelerate();
        movementControler.InstaStop();
        yield return WaitForStopOrTimeout();
        
        float magnitude = rigidbody2D.velocity.magnitude;
        Assert.LessOrEqual(magnitude, 0);
    }
    
    [UnityTest]
    public IEnumerator Stop()
    {
        yield return Accelerate();
        movementControler.Stop();
        yield return WaitForStopOrTimeout();
        
        float magnitude = rigidbody2D.velocity.magnitude;
        Assert.LessOrEqual(magnitude, 0);
    }
    
    /*
     * This test doesn't pass.
     * There are two solutions.
     * First: Fix current solution.
     * Second: if any force will be added then stop Stopping coroutine.
     */
    [UnityTest]
    public IEnumerator StopWithInterjectSpeed()
    {
        yield return Accelerate();
        movementControler.Stop();
        
        Vector3 oppositeVelocity = -rigidbody2D.velocity;
        rigidbody2D.velocity = oppositeVelocity * 2.5f;
        yield return WaitForStopOrTimeout();
        
        float magnitude = rigidbody2D.velocity.magnitude;
        Assert.LessOrEqual(magnitude, 0);
    }
    
    [UnityTest]
    public IEnumerator RotateLeft()
    {
        yield return RotatingSimple(new Vector3(-1, 0, 0));
    }
    
    [UnityTest]
    public IEnumerator RotateRight()
    {
        yield return RotatingSimple(new Vector3(1, 0, 0));
    }

    private IEnumerator RotatingSimple(Vector3 expectedVector)
    {
        float deltaTime = 0;
        float expectedTime = 90 / movementControler.RotatingAccelerationDegree;

        while (deltaTime <= expectedTime) 
        {
            yield return null;
            movementControler.Rotate(expectedVector);
            deltaTime += Time.deltaTime;
        }
        
        Assert.True(movementControler.transform.up == expectedVector, $"transofmr.up equal: {movementControler.transform.up}\n expected: {expectedVector}");
    }
    
    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
    
    private IEnumerator Accelerate()
    {
        float deltaTime = 0;
        float expectedTime = (movementControler.MovingAcceleration / movementControler.MovingAcceleration) *
                             ACCELERATION_TOLLERANCY_TIME_MULTIPLITER;
        while (deltaTime <= expectedTime) 
        {
            yield return null;
            movementControler.Accelerate(1);
            deltaTime += Time.deltaTime;
        }
    }

    private IEnumerator WaitForStopOrTimeout()
    {
        float timeOutTimer = 0;
        while (rigidbody2D.velocity.magnitude >= 0 && timeOutTimer <= MAX_TIME_OUT)
        {
            yield return null;
            timeOutTimer += Time.deltaTime;
        }
    }
}