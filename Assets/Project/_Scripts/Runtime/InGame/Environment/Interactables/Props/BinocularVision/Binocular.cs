using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Interfaces;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.BinocularVision
{
    public class Binocular : InteractableBase, IInteractable
    {
        public bool Activated { get; set; }
        public MiniCamera MiniCamera;

        private bool _canInteract;

        public void StartInteraction()
        {
            MiniCamera.gameObject.SetActive(true);
            MiniCamera.OnMiniCameraOpenedHandler();
        }

        public void EndInteraction()
        {
            MiniCamera.OnMiniCameraClosedHandler();
        }
    }
}
