using Project._Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Project._Scripts.GameCore.UISystem.Elements.DialogueElements
{
  public class DialogueOption : MonoBehaviour
  {
    public DialogueData DialogueData;
    public TMP_Text OptionText;
    public GameObject SelectionObject;
    public DialogueOptionsHolder Holder { get; set; }
    public int Index { get; set; }

    public void Initialize(string text)
    {
      OptionText.text = text;
    }

    public void ToggleSelection(bool condition)
    {
      if (condition == true)
      {
        Holder.DialogueOptions.ForEach(x => x.ToggleSelection(false)); 
      }
      SelectionObject.SetActive(condition);
    }

    public void Select()
    {
      ToggleSelection(true);
      Holder.CurrentSelectionIndex = Index;
    }
  }
}
