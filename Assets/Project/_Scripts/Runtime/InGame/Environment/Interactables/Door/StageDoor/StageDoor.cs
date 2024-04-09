using Project._Scripts.Runtime.CharacterController.CharacterStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Door
{
  public class StageDoor : MonoBehaviour
  {
    private Animator _animator;
    private CharacterStateMachine _characterStateMachine;
    private static readonly int Open = Animator.StringToHash("IsOpen");
    private static readonly int Transport = Animator.StringToHash("Transport");

    public bool IsOpen { get; set; }

    private void Awake()
    {
      _animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
      IsOpen = true;
      _animator.SetBool(Open, IsOpen);
    }

    public void StageCleared()
    {
      _characterStateMachine.Animator.SetTrigger(Transport);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(!IsOpen) return;
      
      if(other.gameObject.layer != LayerMask.NameToLayer("Lancelot")) return;
      
      _characterStateMachine = other.gameObject.GetComponent<CharacterStateMachine>();
      StageCleared();
    }
  }
}
