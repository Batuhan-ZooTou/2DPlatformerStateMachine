using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{

    private Vector2 detectedPosition;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int xInput;
    private int yInput;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        //when climb anims end
        player.Anim.SetBool("ledgeClimb", false);
        player.Anim.SetBool("inAir", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
        //when hold anim ends
        player.Anim.SetBool("hang",true);
    }

    public override void Enter()
    {
        base.Enter();
        
        core.Movement.SetVelocityZero();   //when hanging
        player.transform.position = detectedPosition;
        cornerPos =DetermineCornerPosition();       //gets pos of corner

        startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerData.startOffSet.x), cornerPos.y - playerData.startOffSet.y);
        stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerData.stopOffSet.x), cornerPos.y + playerData.stopOffSet.y);

        player.transform.position = startPos;       // transform player hanging pos
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else
            {
            stateMachine.ChangeState(player.IdleState);

            }
        }
        //if animation doesnt end checks for ýnput and change states depends on it
        else
        {
            xInput = player.InputHandler.NormInputX;    //for climg 
            yInput = player.InputHandler.NormInputY;       //for dropping
            jumpInput = player.InputHandler.JumpInput;      //for wall jumping

            core.Movement.SetVelocityZero();
            player.Anim.SetFloat("yvelocity", 0);
            player.transform.position = startPos;
            player.Anim.SetBool("inAir", true);

            //for climb
            if (xInput == core.Movement.FacingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();    //for ledges that can be climbable while crouched
                isClimbing = true;
                player.Anim.SetBool("ledgeClimb", true);
            }
            //for dropping
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                player.Anim.SetBool("hang", false);
                stateMachine.ChangeState(player.InAirState);
            }
            // for wall jumping
            else if (jumpInput && !isClimbing)
            {
                player.Anim.SetBool("hang", false);
                player.WallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.WallJumpState);
            }
        }
        
        
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPosition = pos;
    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * core.Movement.FacingDirection * 0.015f),Vector2.up,playerData.standColliderHeight+4f, core.CollisionSenses.WhatIsGround);

    }
    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, core.CollisionSenses.WallCheckRadius, core.CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * core.Movement.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheck.position + (Vector3)(workspace), Vector2.down, core.CollisionSenses.LedgeCheck.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheck.position.y - yDist);
        return workspace;
    }
}
