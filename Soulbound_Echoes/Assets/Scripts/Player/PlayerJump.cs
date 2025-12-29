using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : PlayerState
{
    public PlayerJump(PlayerMovement player) : base(player) { }


    public override void Enter()
    {
        base.Enter();

        animator.SetBool("isJumping", true);

        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpForce);

        JumpPressed = false;
        JumpReleased = false;
    }

    public override void Update()
    {
        base.Update();

        if(player.isGrounded && player.rb.linearVelocity.y <= 0)
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.ApplyVariableJump();

        if(JumpReleased && player.rb.linearVelocity.y > 0)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.rb.linearVelocity.y * player.jumpMultiplier);
            JumpReleased = false;
        }

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        player.rb.linearVelocity = new Vector2(speed * player.facingDirection, player.rb.linearVelocity.y);
    }


    public override void Exit()
    {
        base.Exit();

        animator.SetBool("isJumping", false);
    }

}