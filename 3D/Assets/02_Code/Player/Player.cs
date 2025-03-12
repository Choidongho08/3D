using UnityEngine;
using _02_Code.Core.Entities.FSM;
using Code.SO;

namespace _02_Code.Player
{
    public class Player : Entity
    {
        [SerializeField] private PlayerInputSO playerInput;
        
        private EntityMover _movement;
        protected override void Awake()
        {
            base.Awake();
            _movement = GetCompo<EntityMover>();
            playerInput.OnMovementChange += HandleMovementChange;
            playerInput.OnMouseLookChange += HandleMouseLookChange;
        }
        
        private void OnDestroy()
        {
            playerInput.OnMovementChange -= HandleMovementChange;
            playerInput.OnMouseLookChange -= HandleMouseLookChange;
        }

        private void HandleMovementChange(Vector2 movementInput)
        {
            _movement.SetMovementDirection(movementInput);
        }
        private void HandleMouseLookChange(Vector2 mouseLookInput)
        {
            _movement.SetMouseLookInput(mouseLookInput);
        }
    }
}