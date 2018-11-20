﻿namespace Player.State
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
    }
}
