using Project._Scripts.Runtime.CharacterController.CharacterStateMachine;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
{
    public class Trampoline : InteractableBase, IInteractable
    {
        public CharacterStateMachine CharacterStateMachine { get; set; }
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
    
        [Range(5f,75f)]public float Force = 45f;
        private static readonly int Interact = Animator.StringToHash("Interact");
        public bool Activated { get; set; }

        private void Start()
        {
            CharacterStateMachine = FindObjectOfType<CharacterStateMachine>();
            _rigidbody2D = CharacterStateMachine.Rigidbody2D;
            _animator = GetComponent<Animator>();
        }

    
    
        public void StartInteraction()
        {
            CharacterStateMachine.CurrentState.SwitchState(CharacterStateMachine.StateFactory.Jump(Vector2.zero, true));
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            // _rigidbody2D.AddForce(Vector2.up * (Force * (Mathf.Abs(CharacterStateMachine.FallingVelocity / 5.75f) +.025f)), ForceMode2D.Impulse);
            
            _animator.SetTrigger(Interact);

            ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Trampoline");

            Activated = true;
        
        }
        public void EndInteraction()
        {
            Activated = false;
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            StartInteraction();
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            EndInteraction();
        }
    }
}
