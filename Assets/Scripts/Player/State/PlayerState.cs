using UnityEngine;

namespace Player.State
{
    public abstract class PlayerState
    {
        public static readonly PlayerState Standing;
        protected static readonly PlayerState Walking;
        protected static readonly PlayerState Jumping;
        protected static readonly PlayerState Falling;
        protected static readonly PlayerState Ducking;

        static PlayerState()
        {
            Standing = new PlayerStanding();
            Walking = new PlayerWalking();
            Jumping = new PlayerJumping();
            Falling = new PlayerFalling();
            Ducking = new PlayerDucking();
        }

        public abstract void Enter(Player player);

        public abstract PlayerState HandleInput();

        public abstract void UpdatePhysics(Player player);

        protected void ApplyGravityBoost(Player player, float boost=1.6f) {
            player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * boost * Time.deltaTime;
        }

        protected void HandleHorizontalInput(Player player, float boost=1f) {
            var horizontal = Input.GetAxis(Axis.Horizontal);

            // If the player is changing direction  or hasn't reached MaxSpeedX yet
            if (horizontal * player.Rigidbody.velocity.x < player.MaxSpeedX)
            {
                player.Rigidbody.AddForce(Vector2.right * horizontal * boost * player.MoveForceX);
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
