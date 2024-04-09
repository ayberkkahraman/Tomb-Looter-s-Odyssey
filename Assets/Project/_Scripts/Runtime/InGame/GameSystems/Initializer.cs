using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Project._Scripts.Runtime.InGame.GameSystems
{
  public class Initializer : MonoBehaviour
  {
    public GameObject Player;
    public UnityEvent InitializeEvents;
    private bool _active;
    public void ANIM_EVENT_Checkpoint()
    {
      if (PlayerPrefs.HasKey("Checkpoint"))
      {
        Player.SetActive(true);
      }
    }
    public void ANIM_EVENT_Init()
    {
      if(SceneManager.GetActiveScene().buildIndex == 0) return;
      
      InitializeEvents?.Invoke();
    }
    
    public void ANIM_EVENT_CaveInit()
    {
      if(SceneManager.GetActiveScene().buildIndex == 1) return;
      
      InitializeEvents?.Invoke();
    }
    
    public void ANIM_EVENT_ACTIVATE()
    {
      _active = true;
    }

    public void Update()
    {
      if (!_active)
        return;

      if (!Input.GetKeyDown(KeyCode.R))
        return;
      
      PlayerPrefs.DeleteKey($"GORBAG DialogueIndex");
      PlayerPrefs.DeleteKey($"GORBAG IsInteractable");
      PlayerPrefs.DeleteKey($"Checkpoint");
      PlayerPrefs.DeleteKey($"Coin");
      SceneManager.LoadScene("Prologue");
      _active = false;
    }
  }
}
