using Player.State;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public float MoveForceX = 200;

        public float MaxSpeedX = 8;

        public float JumpForce = 600;

        [Header("Set Dynamically")]
        public PlayerState State = PlayerState.Standing;

        public Facing Facing = Facing.Right;

        public Animator Animator;

        public Collider2D Collider;

        public Rigidbody2D Rigidbody;

        private float _distanceToGround;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Collider = GetComponent<Collider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _distanceToGround = Collider.bounds.extents.y;
        }

        private void Update()
        {
            var nextState = State.HandleInput();

            if (nextState == null)
            {
                return;
            }

            State = nextState;
            State.Enter(this);
        }

        private void FixedUpdate()
        {
            State.UpdatePhysics(this);
        }

        public bool OnGround()
        {
            var cast = Physics2D.Raycast(transform.position, -Vector2.up, _distanceToGround + 0.1f);

            return cast.collider != null;
        }
    }
}
