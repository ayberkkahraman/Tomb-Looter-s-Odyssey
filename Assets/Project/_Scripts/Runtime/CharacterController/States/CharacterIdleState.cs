using DG.Tweening;
using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Door;
using Project._Scripts.Runtime.Interfaces;
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
    private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");
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
      Context.ResetCoolDowns();
    }
    public override void FixedUpdateState()
    {

    }
    
    public override void UpdateState()
    {
      Context.UpdateJumpBuffer();
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
      Context.Animator.SetBool(IsGrounded, Context.IsGrounded());

      
      if 
      (
        Context.IsGrounded() && 
        AbilityManager.IsActive("Dash") && 
        InputController.Dash().HasInputTriggered() && 
        Context.DashCooldown >= Context.DefaultDashCooldown
      )
      {
        Context.Animator.SetTrigger(Dash);
        ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Dash");
        Context.Rigidbody2D.AddForce(Context.transform.right * 37.5f, ForceMode2D.Impulse);
        Context.DashCooldown = 0f;
      }
      
      
      if (InputController.Attack().HasInputTriggered() && AbilityManager.IsActive("Attack"))
      {
        if (Context.IsGrounded())
        {
          if (Context.Unit.CurrentCooldown >= Context.Unit.UnitData.AttackCooldown)
          {
            Context.Animator.SetTrigger(Attack);
            Context.Unit.CurrentCooldown = 0;
          }
        }
      }
        
      if(Context.IsMoving()){SwitchState(Factory.Walk());}

      if (!InputController.Jump().HasInputTriggered() || !AbilityManager.IsActive("Jump"))
        return;
      
      Context.CurrentJumpBuffer = Context.DefaultJumpBuffer;
        
      if(Context.CanJump())
        SwitchState(Factory.Jump(Vector2.zero));
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }
  }
}
