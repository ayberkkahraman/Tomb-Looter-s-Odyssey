﻿using System.Linq;
using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.CharacterController.States;
using Project._Scripts.Runtime.Entity.EntitySystem.Entities;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Door;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Props;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Project._Scripts.Runtime.CharacterController.CharacterStateMachine
{
  [RequireComponent(typeof(Animator))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class CharacterStateMachine : MonoBehaviour
  {

    #region State
    public CharacterStateFactory StateFactory { get; set; }
    
    public CharacterBaseState CurrentState { get; set; }

    public CharacterBaseState PreviousState { get; set; }

    public CharacterBaseState NextState { get; set; }
    #endregion

    #region Components
    public Player Unit { get; set; }
    public IInteractable Interactable;
    public Box ClosestBox { get; set; }
    public InteractableBase ClosestInteractable { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public Animator Animator { get; set; }
    public CapsuleCollider2D Collider2D { get; set; }
    #endregion
    #region Fields
    
    [HideInInspector] public Vector2 InputDirection;
    public bool IsMovementButtonPressed { get; set; }

    [Header("Locomotion")]
    [Range(0f,30f)]public float MovementSpeed = 22.5f;
    [Range(0f,10f)]public float SlopeCheckDistance = .5f;
    public Vector2 SlopeNormalPerpendicular { get; set; }
    public float SlopeDownAngle { get; set; }
    public float SlopeDownAnglePrevious { get; set; }
    public float SlopeRightAngle { get; set; }
    public float SlopeRightAnglePrevious { get; set; }
    public bool IsDashing { get; set; }

    public float CurrentMovementSpeed { get; set; }
    [Range(0.2f,2f)]
    public float DashCooldown = .75f;

    [Header("Jump")]
    [Range(0f,50f)]public float JumpForce = 32.5f;
    [Range(0f,.5f)]public float CoyoteTime = .075f;
    [Range(0f,.5f)]public float JumpBuffer = .075f;
    [Range(0f,.25f)]public float JumpCooldown = .2f;

    public float DefaultGravityScale{ get; set; }
    public float DefaultDrag{ get; set; }

    [Space]
    
    [Range(0f,.5f)]public float GroundCheckerDistance;
    public Transform GroundCheckerPoint;
    public LayerMask GroundLayers;

    [Space]
    [Header("Interaction")]
    public LayerMask InteractableLayers;
    private readonly RaycastHit2D[] _interactables = new RaycastHit2D[1];
    
    //BOX PULLING STATS------------------------------------------------------------------------------------------------------
    private static Vector2 Size => new Vector2(.125f, .1f);
    private Vector2 Origin => new Vector2((Vector2.right * (Direction() * .125f)).x, 0f) + (Vector2)transform.position;
    private readonly RaycastHit2D[] _hits = new RaycastHit2D[5];
    //-----------------------------------------------------------------------------------------------------------------------
    public float CurrentCoyoteTimer{ get; set; }
    public float CurrentJumpBuffer { get; set; }
    public float CurrentJumpCooldown { get; set; }
    public float DefaultCoyoteTime { get; set; }
    public float DefaultJumpBuffer { get; set; }
    public float DefaultJumpCooldown { get; set; }
    public float FallingVelocity { get; set; }
    public float DefaultDashCooldown{ get; set; }
    public bool CanRotate { get; set; }
    public bool IsInPlatform { get; set; }

    #endregion

    #region Unity Functions
    private void Awake()
    {
      InitializeComponents();
      InitializeInput();
      InitializeJumpValues();
      InitializeState();
      InitializeGravity();
    }

    private void OnEnable()
    {
      Init();
    }

    private void OnDisable()
    {
      DeInit();
    }

    private void Update()
    {
      CurrentState.UpdateState();

      if (DashCooldown < 1.5f) DashCooldown += Time.deltaTime;
    }

    private void LateUpdate()
    {
      CurrentState.LateUpdateState();
    }

    private void FixedUpdate()
    {
      CurrentState.FixedUpdateState();
    }
    #endregion
    #region Init / DeInit
    private void Init()
    {
      CurrentCoyoteTimer = CoyoteTime;
      CanRotate = true;
    }

    private void DeInit()
    {
      
    }

    public void InitializeGravity()
    {
      DefaultGravityScale = Rigidbody2D.gravityScale;
      DefaultDrag = Rigidbody2D.drag;
    }

    private void InitializeComponents()
    {
      Rigidbody2D = GetComponent<Rigidbody2D>();
      Animator = GetComponent<Animator>();
      Collider2D = GetComponent<CapsuleCollider2D>();
      Unit = GetComponent<Player>();
    }

    private void InitializeInput()
    {
      InputController.ControllerInput.CharacterController.Move.started += MovementConfiguration;
      InputController.ControllerInput.CharacterController.Move.canceled += MovementConfiguration;
      InputController.ControllerInput.CharacterController.Move.performed += MovementConfiguration;
    }
    private void InitializeJumpValues()
    {
      CurrentMovementSpeed = MovementSpeed;
      DefaultCoyoteTime = CoyoteTime;
      DefaultJumpBuffer = JumpBuffer;
      DefaultJumpCooldown = JumpCooldown;
      DefaultDashCooldown = DashCooldown;
      CurrentJumpBuffer = DefaultJumpBuffer;
    }
  #endregion
    
    #region Input
        
    /// <summary>
    /// Gets the player movement input
    /// </summary>
    /// <param name="context"></param>
    private void MovementConfiguration(InputAction.CallbackContext context)
    {
      InputDirection = context.ReadValue<Vector2>();
      
      IsMovementButtonPressed = InputDirection != Vector2.zero;
    }
    #endregion
    #region Condition Configuration
    public int Direction() => IsMovingRight() ? 1 : -1;
    public bool IsMoving() => InputDirection.x != 0;
    public bool IsMovingRight() => transform.rotation.y == 0;
    public bool CanJump() => CurrentCoyoteTimer > 0 && CurrentJumpBuffer >= DefaultJumpBuffer;

    public bool IsGrounded() => Physics2D.OverlapCircleAll(GroundCheckerPoint.position, GroundCheckerDistance, GroundLayers).Length > 0;
    
    #endregion
    #region Locomotion
    public void IncreaseGravity()
    {
      Rigidbody2D.gravityScale = 5;
      Rigidbody2D.drag = 0;
    }

    public void ResetGravity()
    {
      Rigidbody2D.gravityScale = DefaultGravityScale;
      Rigidbody2D.drag = DefaultDrag;
    }
    public void SetRotationAbility(bool canRotate = true) => CanRotate = canRotate;
    public void SetRotation(float horizontalMovement)
    {
      if(!CanRotate) return;
      
      transform.rotation = horizontalMovement switch
      {
        > 0 => new Quaternion(0, 0, 0, 1),
        < 0 => new Quaternion(0, 180, 0, 1),
        _ => transform.rotation
      };
    }
    public void ResetCoolDowns()
    {
      CurrentCoyoteTimer = DefaultCoyoteTime;
      CurrentJumpCooldown = DefaultJumpCooldown;
      CurrentJumpBuffer = DefaultJumpBuffer;
    }
    public void UpdateJumpBuffer()
    {
      if(CurrentJumpBuffer <= 0) return;
      CurrentJumpBuffer -= Time.deltaTime;
    }
    #endregion
    #region State Configuritaion
    private void InitializeState()
    {
      StateFactory ??= new CharacterStateFactory(this);
      CurrentState = StateFactory.Idle();
      CurrentState.EnterState();
      CurrentState.InitializeState();
    }
    #endregion
  }
}
