using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputControler : MonoBehaviour
{
    private MovementController movementController;
    private WeaponControler weaponControler;

    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
        
        movementController = GetComponent<MovementController>();
        weaponControler = GetComponent<WeaponControler>();
        playerInput.Enable();

        playerInput.Ship.Shoot.performed +=
            (ctg) => weaponControler.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update()
    {
        movementController.Rotate(playerInput.Ship.Moving.ReadValue<Vector2>());
        movementController.Accelerate(playerInput.Ship.Acceleration.ReadValue<float>());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementController.Stop();
        }
    }
}
