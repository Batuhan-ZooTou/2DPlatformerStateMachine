using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData",menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity=10f;  //move speed
    [Header("Jump State")]
    public float jumpVelocity=15f;     // jump height
    public int amountOfJump = 1;    // aditional jump
    [Header("In Air State")]
    public float coyoteTime=0.2f;   //timer for jump right after falling from edge
    public float variableJumpHeightMultiplier=0.4f;     //decrease dash height
    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;    
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;
    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime=0.1f;     //timer for jump right after letting go of wall
    public Vector2 wallJumpAngle = new Vector2(1, 2);   // for 45 deg jump cos/sin
    [Header("Ledge Climb State")]
    public Vector2 startOffSet;     //climb pos for x and y
    public Vector2 stopOffSet;      //after climb pos for x and y
    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag=10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.7f;
    [Header("Crouch State")]
    public float crouchMovementVelocity=5f;
    public float crouchColliderHeight=0.8f;
    public float standColliderHeight = 1.27f;
    [Header("Attack State")]
    public float[] attackMovementSpeed;
    public float[] attackDamage;
}
