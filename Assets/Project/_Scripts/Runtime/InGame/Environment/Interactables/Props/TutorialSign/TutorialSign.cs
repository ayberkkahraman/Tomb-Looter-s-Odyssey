using JetBrains.Annotations;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.TutorialSign
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
