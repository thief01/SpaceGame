using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class InputControler : MonoBehaviour
{
    [FormerlySerializedAs("movementController")] [Inject]
    public MovementControler movementControler;
    public WeaponControler weaponControler;

    private PlayerInput playerInput;
    
    private IEnumerator Start()
    {
        yield return null;
        weaponControler = movementControler.GetComponent<WeaponControler>();
        
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Ship.Shoot.performed +=
            (ctg) => weaponControler.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update()
    {
        if (movementControler == null || weaponControler == null)
            return;
        
        movementControler.Rotate(playerInput.Ship.Moving.ReadValue<Vector2>());
        movementControler.Accelerate(playerInput.Ship.Acceleration.ReadValue<float>());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementControler.Stop();
        }
    }
}
