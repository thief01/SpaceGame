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
    
    private MovementController movementController;
    private GameObject gameObject;
    private Rigidbody2D rigidbody2D;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        ZenjectInstaller.Install(Container);
        
        gameObject = new GameObject("Movement controller");
        movementController = Container.InstantiateComponent<MovementController>(gameObject);
        rigidbody2D = movementController.GetComponent<Rigidbody2D>();
    }

    [Test]
    public void Rigidbody()
    {
        Assert.IsNotNull(rigidbody2D);
    }

    [Test]
    public void MovementController()
    {
        Assert.IsNotNull(movementController);
    }

    [UnityTest]
    public IEnumerator Acceleration()
    {
        yield return null;
        movementController.Accelerate(1);
        Assert.Greater(rigidbody2D.velocity.magnitude, 0);
    }
    
    [UnityTest]
    public IEnumerator AccelerationOneSecond()
    {
        yield return Accelerate();
        Assert.GreaterOrEqual(rigidbody2D.velocity.magnitude, movementController.MovingAcceleration);
    }

    [UnityTest]
    public IEnumerator EmergencyStop()
    {
        yield return Accelerate();
        movementController.InstaStop();
        yield return WaitForStopOrTimeout();
        
        float magnitude = rigidbody2D.velocity.magnitude;
        Assert.LessOrEqual(magnitude, 0);
    }
    
    [UnityTest]
    public IEnumerator Stop()
    {
        yield return Accelerate();
        movementController.Stop();
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
        movementController.Stop();
        
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
        float expectedTime = 90 / movementController.RotatingAccelerationDegree;

        while (deltaTime <= expectedTime) 
        {
            yield return null;
            movementController.Rotate(expectedVector);
            deltaTime += Time.deltaTime;
        }
        
        Assert.True(movementController.transform.up == expectedVector, $"transofmr.up equal: {movementController.transform.up}\n expected: {expectedVector}");
    }
    
    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
    
    private IEnumerator Accelerate()
    {
        float deltaTime = 0;
        float expectedTime = (movementController.MovingAcceleration / movementController.MovingAcceleration) *
                             ACCELERATION_TOLLERANCY_TIME_MULTIPLITER;
        while (deltaTime <= expectedTime) 
        {
            yield return null;
            movementController.Accelerate(1);
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