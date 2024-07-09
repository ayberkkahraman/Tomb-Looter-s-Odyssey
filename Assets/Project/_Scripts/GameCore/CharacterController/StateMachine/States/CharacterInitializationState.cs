using System.Collections.Generic;
using System.Linq;
using Project._Scripts.GameCore.CharacterController.StateMachine.Core;

namespace Project._Scripts.GameCore.CharacterController.StateMachine.States
{
  public class CharacterInitializationState : CharacterBaseState
  {
    public CharacterInitializationState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory){}
    protected override void AccelerationConfiguration(float multiplier = 1, bool rotationSmooth = true){}
    protected override void RotationConfiguration(float multiplier = 1){}
    protected override void HandleGravity(float speedMultiplier = 1){}
    public override void EnterState()
    {
      States = new HashSet<CharacterBaseState>
      {
        Factory.Initialize(),
        Factory.Idle(),
        Factory.Walk(),
        Factory.Interaction(),
      };
      
      States.ToList().ForEach(_ => InitializeState());
    }
    public override void FixedUpdateState(){}
    public override void UpdateState(){}
    protected override void ExitState(){}
    public override void CheckSwitchStates(){}
    public override void InitializeState(){}
  }
}