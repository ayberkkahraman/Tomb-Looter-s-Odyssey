using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterIdleState : CharacterBaseState
  {
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dash = Animator.StringToHash("Dash");

    public CharacterIdleState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
    }
    protected override void AccelerationConfiguration(float multiplier = 1, bool rotationSmooth = true)
    {

    }
    protected override void RotationConfiguration(float multiplier = 1)
    {

    }
    protected override void HandleGravity(float speedMultiplier = 1)
    {

    }
    public override void EnterState()
    {
      Context.CurrentMovementSpeed = Context.MovementSpeed;
      Context.Animator.SetBool(IsGrounded, true);
      Context.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void FixedUpdateState()
    {

    }
    
    public override void UpdateState()
    {
      CheckSwitchStates();
    }

    public override void LateUpdateState()
    {

    }
    protected override void ExitState()
    {
    }
    public override void CheckSwitchStates()
    {
      if (InputController.Attack().HasInputTriggered() && AbilityManager.IsActive("Attack"))
      {

          if (Context.Unit.CurrentCooldown >= Context.Unit.UnitData.AttackCooldown)
          {
            Context.Animator.SetTrigger(Attack);
            Context.Unit.CurrentCooldown = 0;
          }
        
      }
        
      if(Context.IsMoving()){SwitchState(Factory.Walk());}
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }
  }
}
