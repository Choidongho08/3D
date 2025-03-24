using _02_Code.Core.Entities.FSM;

namespace _02_Code.Player.States
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
    }
}