using System;
using System.Collections.Generic;
using Project._Scripts.GameCore.CharacterController.StateMachine.Core;
using Project._Scripts.Global.ManagerSystem.Core;
using Project._Scripts.Global.ManagerSystem.ManagerClasses;
using Project._Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.GameCore.NpcSystem.NpcUnits.Base
{
    public abstract class NpcBase : MonoBehaviour
    {
        protected DialogueManager DialogueManager;
        protected Animator Animator;
        
        public CharacterStateMachine Character;

        public Action TriggerInteractCallback;
        public Action EndInteractCallback;
        protected bool IsInteractable { get; set; }

        protected Transform InteractionTarget;
        public CharacterData CharacterData;

        public List<Dialogue> Dialogues;
        [Serializable]
        public struct Dialogue
        {
            public DialogueData DialogueData;
            public UnityEvent OnDialogueStartEvent;
            public UnityEvent OnDialogueEndEvent;
        }

        protected Dialogue CurrentDialogue { get; set; }

        protected int DialogueIndex;

        protected virtual void Start()
        {
            if (DialogueIndex > Dialogues.Count - 1) DialogueIndex = Dialogues.Count - 1;
            CurrentDialogue = Dialogues[DialogueIndex];
            DialogueManager = ManagerCore.Instance.GetInstance<DialogueManager>();
            Animator = GetComponent<Animator>();
        }

        protected void SetRotation()
        {
            transform.rotation = (InteractionTarget.position.x - transform.position.x) switch
            {
                > 0 => new Quaternion(0, 0, 0, 1),
                < 0 => new Quaternion(0, 180, 0, 1),
                _ => transform.rotation
            };
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.layer != LayerMask.NameToLayer($"Player")) return;

            InteractionTarget = other.gameObject.transform;
            Character = InteractionTarget.GetComponent<CharacterStateMachine>();
            
            SetRotation();
            
            if(IsInteractable)
                TriggerInteractCallback?.Invoke();
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.layer != LayerMask.NameToLayer($"Player")) return;

            IsInteractable = false;
            EndInteractCallback?.Invoke();
            
            SetRotation();

            InteractionTarget = null;
        }
        
        public void SetNewDialogue()
        {
            CurrentDialogue = Dialogues[DialogueIndex];
            IsInteractable = true;
        }
    }
}