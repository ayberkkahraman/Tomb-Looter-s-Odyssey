using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.BinocularVision
{
    public class BinocularVision : MonoBehaviour
    {
        public GameObject Tutorial;

        private int _tutorialCompleted;

        public void OnEnable()
        {
            _tutorialCompleted = PlayerPrefs.HasKey("MiniCameraTutorial") ? PlayerPrefs.GetInt("MiniCameraTutorial") : 0;

            Tutorial.SetActive(_tutorialCompleted == 0);
        }

        public void OnDisable()
        {
            _tutorialCompleted = 1;
            PlayerPrefs.SetInt("MiniCameraTutorial", _tutorialCompleted);
        }
        public void ANIM_EVENT_DeactivateMiniCamera()
        {
            gameObject.SetActive(false);
        }
    }
}
