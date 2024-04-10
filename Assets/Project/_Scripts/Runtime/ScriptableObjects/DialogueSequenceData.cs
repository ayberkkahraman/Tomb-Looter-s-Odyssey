using System;
using UnityEngine;

namespace Project._Scripts.Runtime.ScriptableObjects
{
  [CreateAssetMenu(fileName = "DialogueSequenceData", menuName = "DialogueSequence")]
  public class DialogueSequenceData : ScriptableObject
  {
    public SequenceOption[] SequenceOptions;
    
    [Serializable]
    public struct SequenceOption
    {
      [TextArea(3, 10)]
      public string Option;
      public DialogueData DialogueData;
    }
  }
}
