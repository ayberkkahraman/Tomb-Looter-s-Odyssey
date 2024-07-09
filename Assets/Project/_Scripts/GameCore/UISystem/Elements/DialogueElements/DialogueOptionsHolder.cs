using System;
using System.Collections.Generic;
using System.Linq;
using Project._Scripts.Library.InputSystem;
using Project._Scripts.ScriptableObjects;
using UnityEngine;

namespace Project._Scripts.GameCore.UISystem.Elements.DialogueElements
{
  public class DialogueOptionsHolder : MonoBehaviour
  {
    public List<DialogueOption> DialogueOptions { get; set; }
    public int CurrentSelectionIndex { get; set; }
    public int MaxIndexCount { get; set; }

    private bool _optionsActive;

    public Action<DialogueData> DialogueCallback;

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
      
      if(!_optionsActive) return;
      
      if(!InputController.Interact().HasInputTriggered()) return;
      
      DialogueCallback?.Invoke(DialogueOptions[CurrentSelectionIndex].DialogueData);
      CloseOptions();
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

    public void CloseOptions()
    {
      for (int i = 0; i < DialogueOptions.Count; i++)
      {
        DialogueOptions[i].OptionText.text = "";
        DialogueOptions[i].gameObject.SetActive(false);
      }

      CurrentSelectionIndex = 0;
      MaxIndexCount = 0;
    }
    
    public void SetDialogueOptions(int optionsCount = 1)
    {
      MaxIndexCount = optionsCount;
      for (int i = 0; i < optionsCount; i++)
      {
        DialogueOptions[i].gameObject.SetActive(true);
      }
      DialogueOptions[0].Select();
      _optionsActive = true;
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
