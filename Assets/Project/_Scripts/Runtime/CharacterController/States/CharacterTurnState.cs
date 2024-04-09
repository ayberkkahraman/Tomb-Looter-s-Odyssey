using Project._Scripts.Runtime.CharacterController.StateFactory;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterTurnState : CharacterBaseState
  {

    private static readonly int IsTurning = Animator.StringToHash("IsTurning");

    public bool IsTurningRight;
    public CharacterTurnState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, bool isTurningRight = true) : base(currentContext, characterStateFactory)
    {
      IsTurningRight = isTurningRight;
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

    }
    public override void FixedUpdateState()
    {

    }
    public override void UpdateState()
    {
      
    }
    public override void LateUpdateState()
    {

    }
    protected override void ExitState()
    {
      
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
