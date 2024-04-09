using Project._Scripts.Runtime.Library.SubSystems;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using Project._Scripts.Runtime.ScriptableObjects;
using UnityEngine;


namespace Project._Scripts.Runtime.CharacterController.CharacterInteraction
{
  public class CharacterInteraction : MonoBehaviour
  {
    protected DialogueManager DialogueManager;
    private Animator _animator;
    private CharacterStateMachine.CharacterStateMachine _character;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");


    private void OnEnable()
    {
      _character = GetComponent<CharacterStateMachine.CharacterStateMachine>();
      _animator = GetComponent<Animator>();

      DialogueManager = ManagerContainer.Instance.GetInstance<DialogueManager>();
      DialogueManager.OnDialogueStartHandler += RestrictCharacter;
    }
    public void RestrictCharacter(DialogueData dialogueData = null)
    {
      _character.Unit.enabled = false;
      _character.enabled = false;
      _character.Rigidbody2D.velocity = Vector2.zero;
      _character.Animator.SetBool(IsMoving, false);
      _character.Animator.SetBool(IsGrounded, true);
    }

    public void ActivateCharacter(bool activateCamera = true)
    {
      _character.enabled = true;
      _character.Unit.enabled = true;

      if (activateCamera)
      {
        ManagerContainer.Instance.GetInstance<CameraManager>().UpdateFollowTarget(_character.transform);
      }
    }
    
    public void ActivateCharacterAfterDuration(float duration = .5f)
    {
      BaseBehaviour.RunAfterSeconds(duration, () =>
      {
        _character.enabled = true;
        _character.Unit.enabled = true;
      });
    }

    public void InitializePlayer(Transform checkpointTransform)
    {
      transform.position = checkpointTransform.position;
      ActivateCharacter();
      _animator.ResetTrigger($"Init");
    }
  }
}
