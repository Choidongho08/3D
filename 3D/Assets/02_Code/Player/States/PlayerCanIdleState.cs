using _02_Code.Core.Entities.FSM;
using UnityEngine;

namespace _02_Code.Player.States
{
    public class PlayerCanIdleState : PlayerCanAttackState
    {
        private EntityMover _mover;
        
        public PlayerCanIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            _mover.SetMovementDirection(movementKey);
            if (movementKey.magnitude > _inputThreshold)
            {
                _player.ChangeState("MOVE");
            }
        }
    }
}