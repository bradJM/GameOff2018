using UnityEngine;

namespace Player.State
{
    public class PlayerJumping : PlayerState
    {
        private bool _reachedApex;

        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Jump", 0);
            player.Animator.speed = 0;
            _reachedApex = false;
        }

        public override PlayerState HandleInput()
        {
            return _reachedApex ? Falling : null;
        }

        public override void UpdatePhysics(Player player)
        {
            if (Mathf.Approximately(player.Rigidbody.velocity.y, 0) && player.OnGround())
            {
                player.Rigidbody.AddForce(new Vector2(0, player.JumpForce), ForceMode2D.Impulse);
            }

            if (!Input.GetButton("Jump"))
            {
                ApplyGravityBoost(player);
            }

            HandleHorizontalInput(player, boost: 0.15f);
            _reachedApex = player.Rigidbody.velocity.y < 0;
        }
    }
}
