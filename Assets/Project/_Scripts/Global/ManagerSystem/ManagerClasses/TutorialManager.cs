using UnityEngine;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
    public class TutorialManager : MonoBehaviour
    {

        public static TutorialManager Instance;
    
        private void Awake()
        {
            Instance = this;
        }

    }
}
