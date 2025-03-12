using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, PlayerInput.IPlayerActions
    {
        public event Action<Vector2> OnMovementChange;
        public event Action<Vector2> OnMouseLookChange;
        public event Action OnJumpKeyPressed;

        private PlayerInput _playerInput;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.Player.SetCallbacks(this);
            } 
            _playerInput.Player.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            OnMovementChange?.Invoke(move);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpKeyPressed?.Invoke();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            OnMouseLookChange?.Invoke(context.ReadValue<Vector2>());
        }
    }
}