using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using Project._Scripts.Global.Interfaces;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Props.BinocularVision
{
    public class Binocular : InteractableBase, IInteractable
    {
        public bool Activated { get; set; }
        public MiniCamera.MiniCamera MiniCamera;

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
