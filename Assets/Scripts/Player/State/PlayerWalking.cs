using UnityEngine;

namespace Player.State
{
    public class PlayerWalking : PlayerState
    {
        private bool onGround;

        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Walk", 0);
            player.Animator.speed = 1;
        }

        public override PlayerState HandleInput()
        {
            var horizontal = Input.GetAxis(Axis.Horizontal);
            var vertical = Input.GetAxis(Axis.Vertical);

            if (!onGround)
            {
                return Falling;
            }

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
            this.HandleHorizontalInput(player);
            this.onGround = player.OnGround();
        }
    }
}
