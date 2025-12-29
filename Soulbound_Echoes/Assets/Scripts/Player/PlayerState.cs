using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public abstract class PlayerState
{
    protected PlayerMovement player;
    protected Animator animator;

    protected bool  JumpPressed {get => player.jumpPressed ; set => player.jumpPressed = value;}
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }

    protected bool RunPressed => player.runPressed;

    protected Vector2 MoveInput => player.moveInput;

    public PlayerState(PlayerMovement player)
    {
        this.player = player;
        this.animator = player.animator;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
