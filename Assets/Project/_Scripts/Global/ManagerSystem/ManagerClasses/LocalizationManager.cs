using UnityEngine;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;
        public enum Language
        {
            ENGLISH = 0,
            TURKCE = 1,
            CountOfLanguages

        }

        public Language language;

        public void Awake()
        {
            if(Instance == null) { Instance = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(this);
        }
    }
}

