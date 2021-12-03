using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //superState

    // variables
    #region
    // ýnputs
    protected int xInput;
    protected int yInput;
    private bool dashInput;
    private bool grabInput;
    private bool JumpInput;
    private bool attackInput;
    private bool blockInput;

    //checks
    protected bool isTouchingCeiling;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    #endregion

    /// <summary>
    /// contructor
    /// </summary>
    /// <param name="player"></param>
    /// <param name="stateMachine"></param>
    /// <param name="playerData"></param>
    /// <param name="animBoolName"></param>
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {


    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingLedge = core.CollisionSenses.Ledge;
        isTouchingCeiling = core.CollisionSenses.Ceiling;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOffJumpLeft(); // when grounded resets jump
        player.DashState.ResetCanDash();        // when grounded reset dash
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //read ýnputs
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        JumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        attackInput = player.InputHandler.AttackInput;
        blockInput = player.InputHandler.BlockInput;
        

        if (attackInput && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PlayerAttackState);
        }
        else if ( blockInput && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PlayerBlockState);
        }
        //when read jump ýnput
        else if (JumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            Debug.Log("jump");
            stateMachine.ChangeState(player.JumpState );
        }
        //when falling without ýnput
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        //when near wall to grab it
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        //when dash ýnput reads
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
