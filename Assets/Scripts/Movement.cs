using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool shouldJump = false;

    public float moveForceX = 200f;
    public float maxSpeedX = 8f;
    public float jumpForce = 600f;


    private Transform groundCheck;
    private bool grounded = true; // TODO add logic in to check for this
    private Animator anim;
    private Rigidbody2D body;


    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Awake() {
        groundCheck = transform.Find("groundCheck");
    }


    void Update() {
        if (Input.GetButtonDown("Jump") && grounded)
            shouldJump = true;
    }


    void FixedUpdate() {
        // Horizontal movement
        float axisX = Input.GetAxis(Axis.Horizontal);

        // Extra friction if you're not pressing a direction
        if (grounded && Mathf.Approximately(axisX, 0))
            body.velocity = new Vector2(body.velocity.x * 0.9f, body.velocity.y);

        // If the player is changing direction  or hasn't reached maxSpeed yet:
        if (axisX * body.velocity.x < maxSpeedX)
            body.AddForce(Vector2.right * axisX * moveForceX);

        if (Mathf.Abs(body.velocity.x) > maxSpeedX)
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeedX, body.velocity.y);


        // Vertical Movement
        if (shouldJump) {
            body.AddForce(new Vector2(0f, jumpForce));
            shouldJump = false;
        }
        if (body.velocity.y < 0 || !Input.GetButton("Jump")) // falling boost
            body.velocity += Vector2.up * Physics2D.gravity.y * 1.6f * Time.deltaTime;

    }

}