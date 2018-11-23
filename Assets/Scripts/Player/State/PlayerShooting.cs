using UnityEngine;

namespace Player.State
{
    public class PlayerShooting : PlayerState
    {
        public override void Enter(Player player)
        {
        }

        public override PlayerState HandleInput()
        {
            return Standing;
        }

        public override void UpdatePhysics(Player player)
        {
            var projectile = player.SpawnProjectile();
            var direction = player.Facing == Facing.Left ? Vector2.left : Vector2.right;

            projectile.GetComponent<Rigidbody2D>().velocity = direction * 20f;
            projectile.transform.position = player.transform.position;
            player.LastShotTime = Time.time;
        }
    }
}
