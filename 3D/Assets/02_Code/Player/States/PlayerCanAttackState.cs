using _02_Code.Core.Entities.FSM;

namespace _02_Code.Player.States
{
    public class PlayerCanAttackState : PlayerState
    {
        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnAttackKeyPressed += HandleAttackPressed;
        }

        public override void Exit()
        {
            _player.PlayerInput.OnAttackKeyPressed -= HandleAttackPressed;
            base.Exit();          
        }

        private void HandleAttackPressed()
        {
            _player.ChangeState("ATTACK");
        }
    }
}