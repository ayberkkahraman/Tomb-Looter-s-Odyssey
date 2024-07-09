using System.Collections.Generic;
using UnityEngine;

namespace Project._Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public List<Dialogue> Dialogues;
        public DialogueOptionData DialogueOptionData;

        [System.Serializable]
        public struct Dialogue
        {
            public CharacterData CharacterData;

            [TextArea(3, 10)]
            public string Sentence;
        }
    }
}