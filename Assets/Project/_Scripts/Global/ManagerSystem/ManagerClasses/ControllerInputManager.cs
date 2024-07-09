using Project._Scripts.Library.InputSystem;
using UnityEngine;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
    public class ControllerInputManager : MonoBehaviour
    {
        private void Awake()
        {
            InputController.CreateControllerInput();
        }

        private void OnEnable()
        {
            InputController.InitializeControllerInput();
        }

        private void OnDisable()
        {
            InputController.DeInitializeControllerInput();
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.R))
        //     {
        //         SceneTransitionManager.LoadScene(SceneTransitionManager.GetActiveScene().buildIndex);
        //     }
        //     if (Input.GetKeyDown(KeyCode.Y))
        //     {
        //         SceneTransitionManager.LoadScene(SceneTransitionManager.GetActiveScene().buildIndex+1);
        //     }
        //     if (Input.GetKeyDown(KeyCode.Backspace))
        //     {
        //         SceneTransitionManager.LoadScene(SceneTransitionManager.GetActiveScene().buildIndex-1);
        //     }
        // }

    }
}
