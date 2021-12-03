using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // main script for player 
   public PlayerStateMachine StateMachine { get; private set; }

    //all the player states
    #region
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PlayerAttackState { get; private set; }
    public PlayerBlockState PlayerBlockState { get; private set; }
    #endregion

    //checks
    #region
    
    #endregion

    //components
    #region
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB2D { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }           //dash direction with mouse
    public Transform Transform { get; set; }
    public CapsuleCollider2D MovementCollider { get; private set; }         //to change players colliders 
    public PlayerSwordHitBox SwordHitBox { get; private set; }
    
    #endregion

    //workspace
    [SerializeField]
    private PlayerData playerData;          //values that can be changed from unity
    private Vector2 workspace;          //vector2 that to work on when needs to be changed

    //calling methods
    #region
    /// <summary>
    /// gets scripts for all states
    /// </summary>
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        SwordHitBox = GetComponentInChildren<PlayerSwordHitBox>();

        // change state every frame
        StateMachine = new PlayerStateMachine();

        //states
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PlayerAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        PlayerBlockState = new PlayerBlockState(this, StateMachine, playerData, "block");
    }
    /// <summary>
    /// gets components
    /// </summary>
    private void Start()
    {
        Transform = GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB2D = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<CapsuleCollider2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        StateMachine.Initialize(IdleState);
 
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();        // changes the current state       
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();      //changest current state with pyhsics
    }
    #endregion
    // others
    #region
    public void SetColliderHeight(float height)
    {

        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x,height);

        center.y += (height - MovementCollider.size.y) / 2;
        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    /// <summary>
    /// calculates climbable ledges corner to transform
    /// </summary>
    /// <returns></returns>
   

    /// <summary>
    /// if there is trigger animation call that state method 
    /// </summary>
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void AnimationActionTrigger() => StateMachine.CurrentState.AnimationActionTrigger();
    
    #endregion
}
