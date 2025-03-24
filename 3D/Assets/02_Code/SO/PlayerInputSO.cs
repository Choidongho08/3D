using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.SO
{
    public delegate void ValueChange();
    
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, PlayerInput.IPlayerActions
    {
        public Vector2 MovementKey { get; private set; }
        public Vector2 MouseLookKey { get; private set; }

        public event ValueChange MouseLookValueChange;
        
        public event Action OnJumpKeyPressed;
        public event Action OnAttackKeyPressed;

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
            MovementKey = move;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpKeyPressed?.Invoke();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            Vector2 prevValue = MouseLookKey;
            MouseLookKey = context.ReadValue<Vector2>();
            
            if(prevValue != MouseLookKey)
                MouseLookValueChange?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackKeyPressed?.Invoke();
        }
    }
}