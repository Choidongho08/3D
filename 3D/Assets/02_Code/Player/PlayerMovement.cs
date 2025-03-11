using System;
using UnityEngine;

namespace _02_Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f, gravity = -9.81f;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Transform parentTransform;
        
        public bool IsGround => characterController.isGrounded;
        
        
    }
}