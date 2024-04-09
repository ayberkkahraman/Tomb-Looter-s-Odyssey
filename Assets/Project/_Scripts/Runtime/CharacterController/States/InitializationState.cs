using System.Collections.Generic;
using System.Linq;
using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.Library.SubSystems;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class InitializationState : CharacterBaseState
  {
    public InitializationState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
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
      States = new HashSet<CharacterBaseState>
      {
        Factory.Initialize(),
        Factory.Idle(),
        Factory.Jump(Vector2.zero),
        Factory.Walk(),
        Factory.Turn(),
        Factory.Fall(),
        Factory.Interaction(),
        Factory.Dead(),
      };
      
      States.ToList().ForEach(_ => InitializeState());

      SwitchState(Factory.Jump(Vector2.up));

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
