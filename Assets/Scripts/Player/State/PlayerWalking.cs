using UnityEngine;

namespace Player.State
{
    public class PlayerWalking : PlayerState
    {
        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Walk", 0);
            player.Animator.speed = 1;
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

            return Mathf.Approximately(horizontal, 0) ? Standing : null;
        }

        public override void UpdatePhysics(Player player)
        {
            var horizontal = Input.GetAxis(Axis.Horizontal);

            // If the player is changing direction  or hasn't reached MaxSpeedX yet
            if (horizontal * player.Rigidbody.velocity.x < player.MaxSpeedX)
            {
                player.Rigidbody.AddForce(Vector2.right * horizontal * player.MoveForceX);
            }

            if (Mathf.Abs(player.Rigidbody.velocity.x) > player.MaxSpeedX)
            {
                player.Rigidbody.velocity = new Vector2(Mathf.Sign(player.Rigidbody.velocity.x) * player.MaxSpeedX,
                    player.Rigidbody.velocity.y);
            }

            if (horizontal < 0 && player.Facing == Facing.Right)
            {
                Utils.Flip(player.transform);
                player.Facing = Facing.Left;
            }
            else if (horizontal > 0 && player.Facing == Facing.Left)
            {
                Utils.Flip(player.transform);
                player.Facing = Facing.Right;
            }
        }
    }
}
