using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.InGame.GameSystems
{
  public class AsyncSceneLoader : MonoBehaviour
  {
    [Header("Menu Screens")]
    public GameObject LoadingScreen;
    [FormerlySerializedAs("Play")] public GameObject PlayScreen;

    [Header("Slider")]
    [SerializeField] private Slider LoadingSlider;
    [Range(0f, 10f)] [SerializeField] private float LoadSpeed = 10f;

    private bool _activated;

    private void OnEnable()
    {
      StartCoroutine(LoadLevelWithSlider());
    }
    

    private void Update()
    {
      if (!_activated)
        return;

      if (!Input.GetKeyDown(KeyCode.Space))
        return;
      PlayScreen.SetActive(false);
      SceneManager.LoadScene("Prologue");
      _activated = false;

    }

    private IEnumerator LoadLevelWithSlider()
    {
      float sliderValue = 0;

      while (sliderValue < 1f)
      {
        sliderValue += Time.deltaTime * LoadSpeed;
        
        LoadingSlider.value = sliderValue;

        yield return null;
      }
      LoadingScreen.SetActive(false);
      PlayScreen.SetActive(true);
      _activated = true;
    }
  }
}
