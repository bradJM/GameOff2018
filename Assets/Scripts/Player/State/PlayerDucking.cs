using UnityEngine;

namespace Player.State
{
    public class PlayerDucking : PlayerState
    {
        public override void Enter(Player player)
        {
            player.Animator.CrossFade("Player_Duck", 0);
            player.Animator.speed = 0;
        }

        public override PlayerState HandleInput()
        {
            var vertical = Input.GetAxis(Axis.Vertical);

            if (Input.GetButtonDown("Jump"))
            {
                return Jumping;
            }

            return vertical >= -0.75f ? Standing : null;
        }

        public override void UpdatePhysics(Player player)
        {
            player.Rigidbody.velocity = Vector2.zero;
        }
    }
}
