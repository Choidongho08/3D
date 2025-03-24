using _02_Code.Core.StatSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _02_Code.Core.Entities.FSM
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float mouseSensitivity = 1f;
        [SerializeField] private StatSO moveSpeedStat, jumpPowerStat;
        
        #region Member Field

        private Vector3 _movementDirection;
        private Vector2 _mouseLookInput;
        private Vector3 _velocity;
        private float _verticalVelocity;
        private float _moveSpeed = 8f;
        private float _moveSpeedMultiplier;
        private float _jumpPower;
        private float xRotation;
        private float yRotation;
        
        private Rigidbody _rbCompo;
        private EntityStat _statCompo;
        private Entity _entity;
        
        public Vector3 Velocity => _velocity;

        #endregion
       
        public bool IsGround => characterController.isGrounded;
        public bool CanManualMove { get; set; } = true; // 넉백, 기절 시 이동 불가

        #region Init Section

        public void Initialize(Entity entity)
        {
            _entity =  entity;
            _rbCompo = entity.GetComponent<Rigidbody>();
            _statCompo = entity.GetComponentInChildren<EntityStat>();
            _moveSpeedMultiplier = 1f;
        }

        public void AfterInit()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange += HandleMoveSpeedChange;
            _statCompo.GetStat(jumpPowerStat).OnValueChange += HandleJumpPowerChange;
            
            _moveSpeed = _statCompo.GetStat(moveSpeedStat).Value;
            _jumpPower = _statCompo.GetStat(jumpPowerStat).Value;
        }
        
        private void OnDestroy()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange -= HandleMoveSpeedChange;
            _statCompo.GetStat(jumpPowerStat).OnValueChange -= HandleJumpPowerChange;
        }

        #endregion
        
        public void SetMovementDirection(Vector2 movementInput)
        {
            _movementDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        }

        public void SetMouseLookInput(Vector2 mouseLookInput)
        {
            _mouseLookInput = mouseLookInput;
        }

        public void Jump() => AddForceToEntity(new Vector3(0f, _jumpPower, 0f));
        
        public void AddForceToEntity(Vector3 force) 
            => _rbCompo.AddForce(force, ForceMode.Impulse);
        
        public void SetMoveSpeedMultiplier(float value) 
            => _moveSpeedMultiplier = value;
        
        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
            => _moveSpeed = current;
        private void HandleJumpPowerChange(StatSO stat, float current, float previous) 
            => _jumpPower = current;

        private void FixedUpdate()
        {
            CalculateMovement();
            ApplyGravity();
            Move();
        }
        
        private void CalculateMovement()
        {
            _velocity = _movementDirection;
            _velocity *= _moveSpeed * Time.fixedDeltaTime * _moveSpeedMultiplier;
        }
        
        private void ApplyGravity()
        {
            if (IsGround && _verticalVelocity < 0)
                _verticalVelocity = -0.03f; //살짝 아래로 당겨주는 힘
            else
                _verticalVelocity += gravity * Time.fixedDeltaTime;
            
            _velocity.y = _verticalVelocity;
        }

        private void Move()
        {
            if (CanManualMove)
            {
                characterController.Move(_velocity);
            }
        }
    }
}