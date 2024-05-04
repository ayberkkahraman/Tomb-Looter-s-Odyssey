using Project._Scripts.Runtime.CharacterController.ScriptableObjects;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Library.SubSystems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project._Scripts.Runtime.CharacterController.StateMachine.Core
{
  [RequireComponent(typeof(Animator))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class CharacterStateMachine : MonoBehaviour
  {
    #region State
    public StateCore Core { get; set; }
    #endregion

    #region Components
    public IInteractable Interactable;
    public Rigidbody2D Rigidbody2D { get; set; }
    public Animator Animator { get; set; }
    public CapsuleCollider2D Collider2D { get; set; }
    #endregion
    #region Fields
    public CharacterLocomotionData CharacterLocomotionData;
    public Vector2 InputDirection { get; set; }
    public bool IsMovementButtonPressed { get; set; }
    public float CurrentMovementSpeed { get; set; }
    #endregion

    #region Unity Functions
    private void Awake() => new Property<string>(_ => InitializeInput()).Subscribe(_ => InitializeComponents(), _ => InitializeState()).Invoke();
    private void Update() => Core.CurrentState.UpdateState();
    private void FixedUpdate() => Core.CurrentState.FixedUpdateState();
    #endregion
    #region Initialization
    private void InitializeState()
    {
      Core = new StateCore();
      Core.StateFactory ??= new CharacterStateFactory(this);
      Core.CurrentState = Core.StateFactory.Idle();
      Core.CurrentState.EnterState();
      Core.CurrentState.InitializeState();
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
    #region Condition Checkers
    public bool IsMoving() => InputDirection != Vector2.zero;
    #endregion
  }
}