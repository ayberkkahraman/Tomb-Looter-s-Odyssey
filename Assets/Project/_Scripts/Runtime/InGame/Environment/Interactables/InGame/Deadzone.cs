using System;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.InGame
{
    public class Deadzone : InteractableBase
    {
        private CameraManager _cameraManager;
        public GameObject DeadScreen;
        public GameObject Player;
        public UnityEvent Fall;
        private void Start()
        {
            _cameraManager = ManagerContainer.Instance.GetInstance<CameraManager>();
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