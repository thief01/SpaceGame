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

    private const float ACCELERATION_TOLLERANCY_TIME_MULTIPLITER = 2;
    
    [UnityTest]
    public IEnumerator AccelerationOneSecond()
    {
        yield return null;

        float deltaTime = 0;
        float expectedTime = (movementController.MovingAcceleration / movementController.MovingAcceleration) *
                             ACCELERATION_TOLLERANCY_TIME_MULTIPLITER;
        while (deltaTime <= expectedTime) 
        {
            yield return null;
            movementController.Accelerate(1);
            deltaTime += Time.deltaTime;
        }
        
        Assert.GreaterOrEqual(rigidbody2D.velocity.magnitude, movementController.MovingAcceleration);
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
}