using Project._Scripts.Runtime.CharacterController.StateFactory;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Door;
using Project._Scripts.Runtime.Library.SubSystems;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.States
{
  public class CharacterTransportState : CharacterBaseState
  {

    public TransporterDoor TargetDoor;
    public bool CanTransport;
    private static readonly int TransportAnimationHash = Animator.StringToHash("Transport");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    public CharacterTransportState(CharacterStateMachine.CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, TransporterDoor targetDoor) : base(currentContext, characterStateFactory)
    {
      TargetDoor = targetDoor;
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
      Transport();
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
      Context.Animator.ResetTrigger(TransportAnimationHash);
      TargetDoor = null;
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
    
    public void Transport()
    {
      Context.Animator.SetBool(IsGrounded, false);
      Context.Animator.SetTrigger(TransportAnimationHash);

      Context.StartCoroutine(BaseBehaviour.RunAfterSecondsCoroutine(.25f, () =>
      {
        Context.transform.position = TargetDoor.TargetDoor.TransportTransform.position;
        SwitchState(Factory.Idle());
      }));
    }
  }
}
