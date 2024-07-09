using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using Project._Scripts.Global.ManagerSystem.Core;
using Project._Scripts.Global.ManagerSystem.ManagerClasses;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Zones
{
    public class DeadZone : InteractableBase
    {
        private CameraManager _cameraManager;
        public GameObject DeadScreen;
        public GameObject Player;
        public UnityEvent Fall;
        private void Start()
        {
            _cameraManager = ManagerCore.Instance.GetInstance<CameraManager>();
            TriggerInteractCallback = Interact;
        }

        private void Interact()
        {
            _cameraManager.UpdateFollowTarget(null);
            Player.GetComponent<Rigidbody2D>().drag = .5f;
            Invoke(nameof(ShakeCamera), .75f);
            Invoke(nameof(ResetStage), 1f);
            
        }

        public void ShakeCamera()
        {
            Fall?.Invoke();
            _cameraManager.ShakeCamera(2.5f, .75f, .075f);
        }

        public void ResetStage()
        {
            Player.SetActive(false);
            DeadScreen.SetActive(true);
        }
    }
}