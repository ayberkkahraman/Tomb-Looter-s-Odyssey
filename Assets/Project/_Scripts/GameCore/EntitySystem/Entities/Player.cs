using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project._Scripts.GameCore.EntitySystem.Entities
{
  public class Player : Unit
  {
    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.K))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
    }
  }
  
}
