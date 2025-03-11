using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, PlayerInput.IPlayerActions
    {
        public event Action OnJumpKeyPressed;

        public Vector3 MoveDirection { get; private set; }

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

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector3>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpKeyPressed?.Invoke();
        }
    }
}