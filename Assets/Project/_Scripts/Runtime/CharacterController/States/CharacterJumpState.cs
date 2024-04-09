using System.Linq;
using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Library.SubSystems;
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
    private int _dashCount;
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
      _dashCount = 0;
      Context.Animator.SetBool(IsGrounded, false);
      Context.Animator.SetTrigger(JumpAnimationHash);
      if (IsMoving)
        return;
      
      ManagerContainer.Instance.GetInstance<PoolManager>().SpawnFromPool("Jump", Context.transform.position, Quaternion.identity);
      Context.Rigidbody2D.AddForce(Vector2.up * Context.JumpForce, ForceMode2D.Impulse);
      ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Jump");
    }
    public override void FixedUpdateState()
    {
      Move();

      Context.FallingVelocity = Context.Rigidbody2D.velocity.y;
    }
    public override void UpdateState()
    {
      if (Context.Rigidbody2D.velocity == Vector2.zero && 
          Physics2D.RaycastAll(Context.transform.position + Vector3.up*.8f, Context.transform.right, 1f, Context.GroundLayers).Length == 0)
      {
        Context.Rigidbody2D.AddForce(new Vector2(Context.Direction()*3, 3) * Context.JumpForce/10, ForceMode2D.Impulse);
      }
      CheckSwitchStates();
      UpdateAirTime();
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
      if (Context.IsGrounded())
      {
        if(Context.Rigidbody2D.velocity.y > 0) return;

        SwitchState(Factory.Idle());
      }

      else
      {
        if (InputController.Dash().HasInputTriggered() && AbilityManager.IsActive("Air Dash") && Context.DashCooldown >= Context.DefaultDashCooldown)
        {
          if(_dashCount > 0) return;

          _dashCount++;
          Context.Rigidbody2D.gravityScale = 0f;
          BaseBehaviour.RunAfterSeconds(.25f,()=>
          {
            Context.Rigidbody2D.gravityScale = Context.DefaultGravityScale;
          });
          Context.Animator.SetTrigger(Dash);
          ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Dash");
          Context.Rigidbody2D.AddForce(Context.transform.right * 50f, ForceMode2D.Impulse);
          Context.DashCooldown = 0f;
        }
      }
    }
    public override void InitializeSubState()
    {
      
    }
    public override void InitializeState()
    {
      
    }

    public void UpdateAirTime()
    {
      Context.CurrentCoyoteTimer -= Time.deltaTime;
      if (Context.CurrentJumpCooldown > 0) Context.CurrentJumpCooldown -= Time.deltaTime;
    }
  }
}
