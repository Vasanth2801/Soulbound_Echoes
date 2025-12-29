using UnityEngine;

public class PlayerIdle : PlayerState
{

    public PlayerIdle(PlayerMovement player) : base(player)
    {
    }

    public override void Enter()
    {
        animator.SetBool("isIdle",true);
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(AttackPressed && combat.canAttack)
        {
            player.ChangeState(player.attackState);
        }
        else if (JumpPressed)
        {
            JumpPressed = false;
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) > 0.1f)
        {
            player.ChangeState(player.moveState);
        }
        else if (MoveInput.y < -0.1f)
        {
            player.ChangeState(player.crouchState);
        }
    }


    public override void Exit()
    {
        animator.SetBool("isIdle", false);
    }
}