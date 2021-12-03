using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{

    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool shouldCheckFlip;
    //private List<IDamageable> detectedDamageable = new List<IDamageable>();
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        setVelocity = false;
        attackCounter++;
        base.Enter();
        player.Anim.SetInteger("attackCounter", attackCounter);
        if (attackCounter>=playerData.attackMovementSpeed.Length)
        {
            attackCounter = 0;
        }

    }
    public override void Exit()
    {
        base.Exit();
        

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        if (shouldCheckFlip)
        {
        core.Movement.CheckIfShouldFlip(xInput);
        }
        if (setVelocity)
        {
            core.Movement.SetVelocityX(velocityToSet * core.Movement.FacingDirection);
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        SetFlipCheck(true);
        SetPlayerVelocity(0f);
        isAbilityDone = true;
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        SetFlipCheck(false);
        SetPlayerVelocity(playerData.attackMovementSpeed[attackCounter]);
    }
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        player.SwordHitBox.CheckMeleeAttack();

    }
    public void SetPlayerVelocity(float velocity)
    {
        core.Movement.SetVelocityX(velocity * core.Movement.FacingDirection);
        velocityToSet = velocity;
        setVelocity = true;

    }
    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;

    }
    

}
