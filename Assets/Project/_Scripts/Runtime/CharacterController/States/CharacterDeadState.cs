using Project._Scripts.Runtime.CharacterController.StateFactory;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterDeadState : CharacterBaseState
  {

    public CharacterDeadState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
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
