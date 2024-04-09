using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterFallState : CharacterBaseState
  {
    #region Animation Sequence
    //
    // private static readonly int IsFallingAnimationHash = Animator.StringToHash("IsFalling");
    // private static readonly int IsLandedAnimationHash = Animator.StringToHash("IsLanded");
    // private static readonly int TimeInAirAnimationHash = Animator.StringToHash("TimeInAir");
    //
    #endregion
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    public CharacterFallState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
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
      Context.CurrentJumpBuffer = Context.DefaultJumpBuffer;
      Context.Animator.SetBool(IsGrounded, false);
    }
    public override void FixedUpdateState()
    {
      Move();
    }
    public override void UpdateState()
    {
      Context.CurrentCoyoteTimer -= Time.deltaTime;
      CheckSwitchStates();
      
      Context.SetRotation(Context.InputDirection.x);
    }
    public override void LateUpdateState()
    {

    }
    protected override void ExitState()
    {
      
    }
    public override void CheckSwitchStates()
    {
      if(Context.IsGrounded()) SwitchState(Factory.Idle());
      
      if (!InputController.Jump().HasInputTriggered())
        return;

      if(Context.CurrentCoyoteTimer > 0)
        SwitchState(Factory.Jump(Context.InputDirection));
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }
    
    public void Move()
    {
      Context.Rigidbody2D.AddForce(new Vector3(Context.InputDirection.x * (Context.CurrentMovementSpeed - Context.Rigidbody2D.velocity.magnitude) * Context.Rigidbody2D.gravityScale, Context.InputDirection.y,0));
    }
  }
}
