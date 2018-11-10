using UnityEngine;

public abstract class PlayerState
{
    public static readonly PlayerState Standing;
    public static readonly PlayerState Walking;
    public static readonly PlayerState Jumping;
    public static readonly PlayerState Ducking;

    static PlayerState()
    {
        Standing = new PlayerStanding();
        Walking = new PlayerWalking();
        Jumping = new PlayerJumping();
        Ducking = new PlayerDucking();
    }

    public abstract void Enter(Player player);

    public abstract PlayerState HandleInput(Player player);

    public abstract void Exit(Player player);
}

public class PlayerStanding : PlayerState
{
    public override void Enter(Player player)
    {
        Debug.Log("Entered PlayerStanding");
        player.Animator.CrossFade("Player_Idle", 0);
        player.Animator.speed = 0;
    }

    public override PlayerState HandleInput(Player player)
    {
        var horizontal = Input.GetAxis(Axis.Horizontal);
        var vertical = Input.GetAxis(Axis.Vertical);

        if (vertical < 0)
        {
            return Ducking;
        }

        return Mathf.Abs(horizontal) > 0 ? Walking : null;
    }

    public override void Exit(Player player)
    {
        Debug.Log("Exited PlayerStanding");
    }
}

public class PlayerWalking : PlayerState
{
    public override void Enter(Player player)
    {
        Debug.Log("Entered PlayerWalking");
        player.Animator.CrossFade("Player_Walk", 0);
        player.Animator.speed = 1;
    }

    public override PlayerState HandleInput(Player player)
    {
        var horizontal = Input.GetAxis(Axis.Horizontal);
        var vertical = Input.GetAxis(Axis.Vertical);
        var position = player.transform.position;
        PlayerState nextState = null;

        position.x += horizontal * Time.deltaTime * player.WalkingSpeed;

        if (vertical < 0)
        {
            nextState = Ducking;
        }
        else if (Mathf.Approximately(position.x, player.transform.position.x))
        {
            nextState = Standing;
        }

        player.transform.position = position;

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

        return nextState;
    }

    public override void Exit(Player player)
    {
        Debug.Log("Exited PlayerWalking");
    }
}

public class PlayerJumping : PlayerState
{
    public override void Enter(Player player)
    {
        Debug.Log("Entered PlayerJumping");
    }

    public override PlayerState HandleInput(Player player)
    {
        Debug.Log("HandleInput PlayerJumping");
        return null;
    }

    public override void Exit(Player player)
    {
        Debug.Log("Exited PlayerJumping");
    }
}

public class PlayerDucking : PlayerState
{
    public override void Enter(Player player)
    {
        Debug.Log("Entered PlayerDucking");
        player.Animator.CrossFade("Player_Duck", 0);
        player.Animator.speed = 0;
    }

    public override PlayerState HandleInput(Player player)
    {
        var vertical = Input.GetAxis(Axis.Vertical);

        return vertical >= 0 ? Standing : null;
    }

    public override void Exit(Player player)
    {
        Debug.Log("Exited PlayerDucking");
    }
}
