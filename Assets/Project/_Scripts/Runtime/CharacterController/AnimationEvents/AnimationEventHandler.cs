using Project._Scripts.Runtime.Library.SubSystems;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.CharacterController.AnimationEvents
{
  public class AnimationEventHandler : MonoBehaviour
  {
    private Animator _animator;
    private CharacterStateMachine.CharacterStateMachine _characterStateMachine;
    
    private static readonly int Dash = Animator.StringToHash("Dash");
    private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Start()
    {
      _animator = GetComponent<Animator>();
      _characterStateMachine = GetComponent<CharacterStateMachine.CharacterStateMachine>();
    }
    public void ANIM_EVENT_Footstep()
    {
      ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Footstep");
    }

    public void EVENT_Block()
    {
      _animator.SetBool(IsBlocking, true);
      BaseBehaviour.RunAfterSeconds(.25f, () =>
      {
        _animator.SetBool(IsBlocking, _characterStateMachine.Unit.IsVulnerable);
        _animator.SetBool(IsMoving, _characterStateMachine.IsMoving());
      });
    }

    public void EVENT_Dash()
    {
      _animator.SetTrigger(Dash);
      _characterStateMachine.Rigidbody2D.AddForce(transform.right * 40f, ForceMode2D.Impulse);
      // _characterStateMachine.DashCooldown = 0f;
    }
  }
}
