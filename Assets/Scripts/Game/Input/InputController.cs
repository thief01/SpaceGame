using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    private MovementController movementController;

    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
        
        movementController = GetComponent<MovementController>();
        playerInput.Enable();
        
        playerInput.Ship.Shoot.performed += (ctg) => Debug.Log("Shoot");
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
