using UnityEngine;

namespace Player.State
{
    public class PlayerFalling : PlayerState
    {
        private bool _reachedGround;

        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Idle", 0);
            player.Animator.speed = 0;
            _reachedGround = false;
        }

        public override PlayerState HandleInput()
        {
            return _reachedGround ? Standing : null;
        }

        public override void UpdatePhysics(Player player)
        {
            ApplyGravityBoost(player);
            HandleHorizontalInput(player, boost: 0.15f);
            _reachedGround = player.OnGround();
        }
    }
}
