using UnityEngine;
using UnityEngine.InputSystem;
using Platformer.Creatures;

namespace Platformer
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void Awake()
        {
            _hero = GetComponent<Hero>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }        
        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Throw();
            }
        }
    }
}
