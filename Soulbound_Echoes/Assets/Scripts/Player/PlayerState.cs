using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public abstract class PlayerState
{
    protected PlayerMovement player;
    protected Animator animator;
    protected Combat combat;

    protected bool  JumpPressed {get => player.jumpPressed ; set => player.jumpPressed = value;}
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }

    protected bool RunPressed => player.runPressed;

    protected bool AttackPressed => player.attackPressed;

    protected Vector2 MoveInput => player.moveInput;

    public PlayerState(PlayerMovement player)
    {
        this.player = player;
        this.animator = player.animator;
        combat = player.combat;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void AttackAnimationFinished() { }
}
