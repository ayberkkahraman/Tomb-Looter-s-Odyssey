using JetBrains.Annotations;
using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using Project._Scripts.Global.Interfaces;
using Project._Scripts.Library.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Props.TutorialSign
{
    public class TutorialSign : InteractableBase, IInteractable
    {
        public string TutorialName;
        [CanBeNull]public GameObject Tutorial;
        public bool Activated { get; set; }
        public bool HasEvent;

        public UnityEvent Event;

        private void Start()
        {
            TriggerInteractCallback = StartInteraction;
            EndInteractCallback = EndInteraction;
        }

        private void Update()
        {
            if (!HasEvent)
                return;
            
            if (!Activated)
                return;

            if (!InputController.Interact().HasInputPerformed())
                 return;

            EndInteraction();
            Destroy(this);
        }
        public void StartInteraction()
        {
            Activated = true;
            if(Tutorial!=null)Tutorial!.SetActive(true);
            Event?.Invoke();
        }
        public void EndInteraction()
        {
            Activated = false;
            if(Tutorial!=null)Tutorial!.SetActive(false);
        }
    }
}
