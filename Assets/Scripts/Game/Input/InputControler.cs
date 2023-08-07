using System.Collections;
using Game.Character;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Input
{
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
                (ctg) => weaponControler.Shoot(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
        }

        private void Update()
        {
            if (movementControler == null || weaponControler == null)
                return;
        
            movementControler.Rotate(playerInput.Ship.Moving.ReadValue<Vector2>());
            movementControler.Accelerate(playerInput.Ship.Acceleration.ReadValue<float>());

            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                movementControler.Stop();
            }
        }
    }
}
