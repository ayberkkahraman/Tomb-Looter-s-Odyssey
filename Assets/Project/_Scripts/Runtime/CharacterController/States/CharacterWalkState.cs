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

    public bool CanMove { get; set; }
    public bool CanRotate { get; set; }
    public CharacterWalkState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
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
      Context.Animator.SetBool(IsGrounded, true);
      Context.Animator.SetBool(IsMoving, true);
      Context.ResetCoolDowns();
    }
    public override void UpdateState()
    {
      CheckSwitchStates();
      
      Context.UpdateJumpBuffer();
      AccelerationConfiguration();

      Context.SetRotation(Context.InputDirection.x);
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
      if (!Context.IsGrounded())
      {
        Context.Animator.SetBool(IsGrounded, Context.IsGrounded());
        if(Context.CanJump() == false)
          SwitchState(Factory.Fall());
      }
      
      if (Context.IsGrounded() && AbilityManager.IsActive("Dash") && InputController.Dash().HasInputTriggered() && Context.DashCooldown >= Context.DefaultDashCooldown)
      {
        ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Dash");
        Context.Animator.SetTrigger(Dash);
        Context.Rigidbody2D.AddForce(Context.transform.right * 30f, ForceMode2D.Impulse);
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
      
      if(!Context.IsMovementButtonPressed && Context.IsGrounded()){SwitchState(Factory.Idle());}

      if (!InputController.Jump().HasInputTriggered())
        return;
      if(! AbilityManager.IsActive("Jump")) return;
      
      Context.CurrentJumpBuffer = Context.DefaultJumpBuffer;
        
      if(Context.CanJump())
        SwitchState(Factory.Jump(Context.InputDirection));
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
      Context.Rigidbody2D.AddForce(new Vector3(Context.InputDirection.x * (Context.CurrentMovementSpeed - Context.Rigidbody2D.velocity.magnitude) * Context.Rigidbody2D.gravityScale, Context.InputDirection.y,0));
    }
  }
}
