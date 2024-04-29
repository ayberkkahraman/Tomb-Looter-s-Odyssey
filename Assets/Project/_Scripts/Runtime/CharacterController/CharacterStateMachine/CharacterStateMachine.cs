using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.CharacterController.States;
using Project._Scripts.Runtime.Entity.EntitySystem.Entities;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public IInteractable Interactable;
    public Rigidbody2D Rigidbody2D { get; set; }
    public Animator Animator { get; set; }
    public CapsuleCollider2D Collider2D { get; set; }
    #endregion
    #region Fields
    
    [HideInInspector] public Vector2 InputDirection;
    public bool IsMovementButtonPressed { get; set; }

    [Header("Locomotion")]
    [Range(0f,30f)]public float MovementSpeed = 22.5f;
    public float CurrentMovementSpeed { get; set; }
    public bool CanRotate { get; set; }

    #endregion

    #region Unity Functions
    private void Awake()
    {
      InitializeComponents();
      InitializeInput();
      InitializeState();
    }

    private void OnEnable()
    {
      Init();
    }
    private void Update()
    {
      CurrentState.UpdateState();
      
      Debug.DrawRay(transform.position, transform.right, Color.red);
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
      CanRotate = true;
    }
    private void InitializeComponents()
    {
      Rigidbody2D = GetComponent<Rigidbody2D>();
      Animator = GetComponent<Animator>();
      Collider2D = GetComponent<CapsuleCollider2D>();
    }

    private void InitializeInput()
    {
      InputController.ControllerInput.CharacterController.Move.started += MovementConfiguration;
      InputController.ControllerInput.CharacterController.Move.canceled += MovementConfiguration;
      InputController.ControllerInput.CharacterController.Move.performed += MovementConfiguration;
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
    public bool IsMoving() => InputDirection != Vector2.zero;
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
