using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float WalkingSpeed = 5;

    [Header("Set Dynamically")]
    public PlayerState State = PlayerState.Standing;

    public Facing Facing = Facing.Right;

    public Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var nextState = State.HandleInput(this);

        if (nextState == null)
        {
            return;
        }

        State.Exit(this);
        State = nextState;
        State.Enter(this);
    }
}
