using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project._Scripts.Runtime.Game.UIElements
{
  public class DialogueOptionsHolder : MonoBehaviour
  {
    public List<DialogueOption> DialogueOptions { get; set; }
    public int CurrentSelectionIndex { get; set; }
    public int MaxIndexCount { get; set; }

    private void Awake()
    {
      DialogueOptions = new List<DialogueOption>();
      GetComponentsInChildren<DialogueOption>(true).ToList().ForEach(x => DialogueOptions.Add(x));

      for (int i = 0; i < DialogueOptions.Count; i++)
      {
        DialogueOptions[i].Index = i;
        DialogueOptions[i].Holder = this;
      }
    }

    public void Update()
    {
      SwitchOption();
    }

    public void SwitchOption()
    {
      if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
      {
        UpdateCurrentIndex(true);
      }
      
      else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
      {
        UpdateCurrentIndex(false);
      }
    }

    public void UpdateCurrentIndex(bool increase)
    {
      if (increase)
      {
        if (CurrentSelectionIndex == MaxIndexCount - 1) CurrentSelectionIndex = 0;
        else CurrentSelectionIndex++;
      }

      else
      {
        if (CurrentSelectionIndex == 0) CurrentSelectionIndex = MaxIndexCount - 1;
        else CurrentSelectionIndex--;
      }
      
      DialogueOptions.Find(x => x.Index == CurrentSelectionIndex).Select();
    }
  }
}
