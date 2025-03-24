using System;
using _02_Code.Core.Entities.FSM;
using UnityEngine;

namespace _02_Code.Player.Front
{
    public class CameraRotate : MonoBehaviour, IEntityComponent
    {
        private Player _player;
        private float xRotation, yRotation;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.PlayerInput.MouseLookValueChange += HandleMouseLook;
        }
        
        private void HandleMouseLook()
        {
            float mouseX = _player.PlayerInput.MouseLookKey.x * _player.MouseSensitivity * Time.deltaTime;
            float mouseY = _player.PlayerInput.MouseLookKey.y * _player.MouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

       
    }
}