using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project._Scripts.Runtime.InGame.GameSystems
{
  public class RestartButton : MonoBehaviour
  {
    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
  }
}
