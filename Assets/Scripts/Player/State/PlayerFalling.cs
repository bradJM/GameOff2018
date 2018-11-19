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
            if (player.Rigidbody.velocity.y < 0 || !Input.GetButton("Jump"))
            {
                player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * 1.6f * Time.deltaTime;
            }

            _reachedGround = player.OnGround();
        }
    }
}
