using TMPro;
using UnityEngine;

namespace Project._Scripts.Runtime.Game.UIElements
{
  public class DialogueOption : MonoBehaviour
  {
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
