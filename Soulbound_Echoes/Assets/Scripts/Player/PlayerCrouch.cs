using UnityEngine;

public class PlayerCrouch : PlayerState
{
    public PlayerCrouch(PlayerMovement player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("isCrouching", true);
        player.SetColliderSlide();
    }

    public override void Update()
    {
        base.Update();

        if(JumpPressed)
        {
            player.ChangeState(player.jumpState);
        }
        else if(MoveInput.y > 0.1f && !player.CheckForCieling())
        {
            player.ChangeState(player.idleState);
        }
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(Mathf.Abs(MoveInput.x) > 0.1f)
        {
            player.rb.linearVelocity = new Vector2(player.facingDirection * player.walkSpeed,player.rb.linearVelocity.y);
        }
        else
        {
            player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);
        }
    }


    public override void Exit()
    {
        base.Exit();

        animator.SetBool("isCrouching", false);
        player.SetColliderNormal();
    }
}
