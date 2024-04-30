using Project._Scripts.Runtime.CharacterController.StateFactory;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterWalkState : CharacterBaseState
  {
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    
    private Vector2 _movementDirection;
    
    private static readonly int DirectionX = Animator.StringToHash("DirectionX");
    private static readonly int DirectionY = Animator.StringToHash("DirectionY");
    public CharacterWalkState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
    }
    protected override void AccelerationConfiguration(float multiplier = 1, bool rotationSmooth = true)
    {
      _movementDirection = Context.InputDirection;
    }
    protected override void RotationConfiguration(float multiplier = 1)
    {

    }
    protected override void HandleGravity(float speedMultiplier = 1)
    {

    }
    public override void EnterState()
    {
      Context.Animator.SetBool(IsMoving, true);
    }
    public override void UpdateState()
    {
      CheckSwitchStates();

      AccelerationConfiguration();
    }
    public override void LateUpdateState()
    {

    }
    public override void FixedUpdateState()
    {
      Move();
    }
    protected override void ExitState()
    {
      Context.Animator.SetBool(IsMoving, false);
    }

    public override void CheckSwitchStates()
    {
      if(!Context.IsMovementButtonPressed){SwitchState(Factory.Idle());}
      else SetAnimations();
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }
    
    /// <summary>
    /// Handles the movement
    /// </summary>
    public void Move()
    {
      Context.Rigidbody2D.MovePosition(Context.Rigidbody2D.position + _movementDirection * (Time.fixedDeltaTime * Context.CurrentMovementSpeed));
    }

    public void SetAnimations()
    {
      Context.Animator.SetInteger(DirectionX, Mathf.RoundToInt(_movementDirection.x));
      Context.Animator.SetInteger(DirectionY, Mathf.RoundToInt(_movementDirection.y));
    }
  }
}
