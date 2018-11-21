using UnityEngine;

namespace Player.State
{
    public class PlayerWalking : PlayerState
    {
        private bool _onGround;

        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Walk", 0);
            player.Animator.speed = 1;
            _onGround = false;
        }

        public override PlayerState HandleInput()
        {
            var horizontal = Input.GetAxis(Axis.Horizontal);
            var vertical = Input.GetAxis(Axis.Vertical);

            if (!_onGround)
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
            HandleHorizontalInput(player);
            _onGround = player.OnGround();
        }
    }
}
