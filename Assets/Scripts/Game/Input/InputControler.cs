using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class InputControler : MonoBehaviour
{
    [Inject]
    public MovementController movementController;
    public WeaponControler weaponControler;

    private PlayerInput playerInput;
    
    private IEnumerator Start()
    {
        yield return null;
        weaponControler = movementController.GetComponent<WeaponControler>();
        
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Ship.Shoot.performed +=
            (ctg) => weaponControler.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update()
    {
        if (movementController == null || weaponControler == null)
            return;
        
        movementController.Rotate(playerInput.Ship.Moving.ReadValue<Vector2>());
        movementController.Accelerate(playerInput.Ship.Acceleration.ReadValue<float>());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementController.Stop();
        }
    }
}
