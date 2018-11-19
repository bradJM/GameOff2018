using UnityEngine;

namespace Player.State
{
    public class PlayerStanding : PlayerState
    {
        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Idle", 0);
            player.Animator.speed = 0;
        }

        public override PlayerState HandleInput()
        {
            var horizontal = Input.GetAxis(Axis.Horizontal);
            var vertical = Input.GetAxis(Axis.Vertical);

            if (Input.GetButtonDown("Jump"))
            {
                return Jumping;
            }

            if (vertical < -0.75f)
            {
                return Ducking;
            }

            return Mathf.Abs(horizontal) > 0 ? Walking : null;
        }

        public override void UpdatePhysics(Player player)
        {
            player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x * 0.9f, player.Rigidbody.velocity.y);
        }
    }
}
