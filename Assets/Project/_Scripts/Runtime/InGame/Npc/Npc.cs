using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using Project._Scripts.Runtime.ScriptableObjects;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Npc
{
  public class Npc : NpcBase
  {
    private static readonly int Interacting = Animator.StringToHash("Interacting");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    
    public Transform TargetTransform;

    public class CharacterTransform
    {
      public Vector3 Position;
      public Quaternion Rotation;

      public CharacterTransform(Vector3 position, Quaternion rotation)
      {
        Position = position;
        Rotation = rotation;
      }
    }

    public CharacterTransform Transform;
    protected override void Start()
    {
      base.Start();
      
      TriggerInteractCallback = Interact;

      DialogueManager.OnDialogueEndHandler += EndInteract;
      
      EndInteractCallback += () =>
      {
        Character = null;
      };
    }

    public void Interact()
    {

      Animator.SetBool(Interacting, true);
      
      Character.enabled = false;
      
      Character.Animator.SetBool(IsMoving, false);
      Character.Animator.Play($"Idle");
      Character.Animator.SetBool(IsGrounded, true);

      ManagerContainer.Instance.GetInstance<DialogueManager>().OnDialogueStartHandler(CurrentDialogue.DialogueData);
    }

    public void Teleport()
    {
      transform.position = TargetTransform.position;
      transform.localRotation = new Quaternion(0, 180, 0, 1);
    }

    public void EndInteract(DialogueData dialogueData)
    {
      if(Character == null) return;
      
      if(dialogueData != CurrentDialogue.DialogueData) return;
      
      ExitDialogue();
      
      CurrentDialogue.OnDialogueEndEvent?.Invoke();
    }

    public void ExitDialogue()
    {
      Animator.SetBool(Interacting, false);

      Character.Animator.SetBool($"IsMoving", Character.IsMoving());
      
      DialogueManager.UIManager.DialogueBox.SetActive(false);

      if(DialogueIndex == Dialogues.Count -1) return;
      
      DialogueIndex++;
      SaveManager.SaveData($"{CharacterData.Name} DialogueIndex", DialogueIndex);
    }
  }
}
