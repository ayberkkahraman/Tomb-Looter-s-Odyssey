using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterJumpState : CharacterBaseState
  {

    private static readonly int JumpAnimationHash = Animator.StringToHash("Jump");
 
    public bool IsMoving;

    private float _verticalVelocity;

    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int Dash = Animator.StringToHash("Dash");
    public CharacterJumpState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, bool isMoving) : base(currentContext, characterStateFactory)
    {
      IsMoving = isMoving;
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
      Context.Animator.SetBool(IsGrounded, false);
      Context.Animator.SetTrigger(JumpAnimationHash);
      if (IsMoving)
        return;
      
      ManagerContainer.Instance.GetInstance<PoolManager>().SpawnFromPool("Jump", Context.transform.position, Quaternion.identity);
      // Context.Rigidbody2D.AddForce(Vector2.up * Context.JumpForce, ForceMode2D.Impulse);
      ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Jump");
    }
    public override void FixedUpdateState()
    {
      Move();
    }
    public override void UpdateState()
    {
      CheckSwitchStates();
      Context.SetRotation(Context.InputDirection.x);
    }
    public override void LateUpdateState()
    {

    }
    protected override void ExitState()
    {
      Context.Animator.ResetTrigger(Dash);
    }
    
    public void Move()
    {
      Context.Rigidbody2D.AddForce(new Vector3(Context.InputDirection.x * (Context.CurrentMovementSpeed - Context.Rigidbody2D.velocity.magnitude) * Context.Rigidbody2D.gravityScale, Context.InputDirection.y,0), ForceMode2D.Force);
    }
    public override void CheckSwitchStates()
    {
      
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }
  }
}
