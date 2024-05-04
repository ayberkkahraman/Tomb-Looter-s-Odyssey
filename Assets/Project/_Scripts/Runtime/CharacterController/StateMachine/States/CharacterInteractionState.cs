using Project._Scripts.Runtime.CharacterController.StateMachine.Core;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.StateMachine.States
{
  public class CharacterInteractionState : CharacterBaseState
  {
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public bool CanMove { get; set; }
    public CharacterInteractionState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, bool canMove) : base(currentContext, characterStateFactory) => CanMove = canMove;
    protected override void AccelerationConfiguration(float multiplier = 1, bool rotationSmooth = true){}
    protected override void RotationConfiguration(float multiplier = 1){}
    protected override void HandleGravity(float speedMultiplier = 1){}
    public override void EnterState()
    {
      Context.Animator.SetBool(IsMoving, false);
      Context.Interactable.StartInteraction();
    }
    public override void FixedUpdateState(){}
    public override void UpdateState()
    {
      CheckSwitchStates();
    }
    protected override void ExitState(){}
    public override void CheckSwitchStates()
    {
      Context.Interactable.EndInteraction();
      SwitchState(Factory.Idle());
    }
    public override void InitializeState(){}
  }
}