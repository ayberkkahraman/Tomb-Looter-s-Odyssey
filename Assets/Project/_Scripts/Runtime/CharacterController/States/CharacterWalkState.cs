using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterWalkState : CharacterBaseState
  {
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dash = Animator.StringToHash("Dash");

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
      Context.Animator.SetBool(IsGrounded, true);
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
      if (AbilityManager.IsActive("Dash") && InputController.Dash().HasInputTriggered())
      {
        ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Dash");
        Context.Animator.SetTrigger(Dash);
        Context.Rigidbody2D.AddForce(Context.transform.right * 30f, ForceMode2D.Impulse);
        // Context.DashCooldown = 0f;
      }
      
      if (InputController.Attack().HasInputTriggered() && AbilityManager.IsActive("Attack"))
      {

          if (Context.Unit.CurrentCooldown >= Context.Unit.UnitData.AttackCooldown)
          {
            Context.Animator.SetTrigger(Attack);
            Context.Unit.CurrentCooldown = 0;
          }
        
      }
      
      
      if(!Context.IsMovementButtonPressed){SwitchState(Factory.Idle());}
      else
      {
        SetAnimations();
      }

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
      Context.Animator.SetFloat(DirectionX, _movementDirection.x);
      Context.Animator.SetFloat(DirectionY, _movementDirection.y);
    }
  }
}
