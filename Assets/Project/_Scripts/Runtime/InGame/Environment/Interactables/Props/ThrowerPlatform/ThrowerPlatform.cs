using System;
using Project._Scripts.Runtime.CharacterController.CharacterStateMachine;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
{
    public class ThrowerPlatform : InteractableBase, IInteractable
    {
        private Animator _animator;
        public CharacterStateMachine CharacterStateMachine { get; set; }
        private Rigidbody2D _rigidbody2D;

        [Range(0f,50f)]public float ThrowForceForward = 35f;
        [Range(0f,50f)]public float ThrowForceUpward = 15f;
        [Range(0f,10f)][SerializeField]private float Cooldown = .425f;
    
        private float _cooldownTimer;
        private int _direction;
        public bool Activated { get; set; }
        private static readonly int Interact = Animator.StringToHash("Interact");

        public void Start()
        {
            _animator = GetComponent<Animator>();
            CharacterStateMachine = FindObjectOfType<CharacterStateMachine>();
            _rigidbody2D = CharacterStateMachine.Rigidbody2D;

            _direction = Math.Abs(transform.eulerAngles.y - 180) < .0001f ? 1 : -1;
            _cooldownTimer = Cooldown;
        }

        public void Update()
        {
            if(_cooldownTimer < Cooldown) { _cooldownTimer += Time.deltaTime; }
        }

        public void StartInteraction()
        {
            if (Activated || !(_cooldownTimer >= Cooldown))
                return;
        
            _animator.SetTrigger(Interact);
            CharacterStateMachine.CurrentState.SwitchState(CharacterStateMachine.StateFactory.Jump(Vector2.zero, true));

            _rigidbody2D.velocity = new Vector2(0, 0);
            _rigidbody2D.AddForce(new Vector2(_direction * ThrowForceForward, ThrowForceUpward), ForceMode2D.Impulse);

            ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("ThrowerPlatform");

            Activated = true;
            _cooldownTimer = 0;
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
