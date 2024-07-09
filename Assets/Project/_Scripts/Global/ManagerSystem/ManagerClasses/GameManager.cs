using Project._Scripts.ScriptableObjects;
using UnityEngine;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
  public class GameManager : MonoBehaviour
  {
    public CharacterData Data;

    private void Start()
    {
      PlayerPrefs.DeleteKey($"{Data.Name} DialogueIndex");
      PlayerPrefs.DeleteKey($"{Data.Name} IsInteractable");
      PlayerPrefs.DeleteKey($"Checkpoint");
      PlayerPrefs.DeleteKey($"Coin");
    }
  }
}
