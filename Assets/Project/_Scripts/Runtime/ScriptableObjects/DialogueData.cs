using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public List<Dialogue> Dialogues;
        public DialogueSequenceData DialogueSequenceData;

        [System.Serializable]
        public struct Dialogue
        {
            public CharacterData CharacterData;

            [TextArea(3, 10)]
            public string Sentence;
        }
    }
}