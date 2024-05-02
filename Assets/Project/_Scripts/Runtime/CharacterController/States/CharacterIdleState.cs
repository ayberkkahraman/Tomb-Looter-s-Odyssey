using Project._Scripts.Runtime.CharacterController.StateFactory;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterIdleState : CharacterBaseState
  {
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
      Context.Rigidbody2D.velocity = Vector2.zero;
    }
    public override void FixedUpdateState()
    {

    }
    
    public override void UpdateState()
    {
      CheckSwitchStates();
    }

    protected override void ExitState()
    {
    }
    public override void CheckSwitchStates()
    {
      if(Context.IsMoving()){SwitchState(Factory.Walk());}
    }
    public override void InitializeState()
    {
      
    }
  }
}
