using Project._Scripts.Runtime.CharacterController.CharacterStateMachine;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Interfaces;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Elevator
{
    public class ElevatorButton : InteractableBase, IInteractable
    {

        public CharacterStateMachine CharacterStateMachine { get; set; }
        public ElevatorPlatform ElevatorPlatform{ get; set; }
        public GameObject ActiveState{ get; set; }
        public GameObject InactiveState{ get; set; }
        public bool Activated { get => _activated; set => _activated = value; }
        private bool _activated;
        
        private void Start()
        {
            ActiveState = transform.Find("Active").gameObject;
            InactiveState = transform.Find("Inactive").gameObject;
            
            CharacterStateMachine = FindObjectOfType<CharacterStateMachine>();
            ElevatorPlatform = GetComponentInParent<ElevatorPlatform>();
            ElevatorPlatform.Button = this;
        }

        public void Interact()
        {
            Activation(ref _activated);
            
            ElevatorPlatform.TargetPosition = Activated ? ElevatorPlatform.DownPoint.position : ElevatorPlatform.TopPoint.position;

            ElevatorPlatform.OnButtonTriggeredHandler();
        }
        

        public void Activation(ref bool activated)
        {
            activated = !activated;

            ActiveState.SetActive(activated);

            InactiveState.SetActive(!activated);
        }

        public void StartInteraction()
        {
            IsInteractable = false;
        }
        public void EndInteraction()
        {
            IsInteractable = true;
        }
    }
}
