using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.Game
{
  public class AudioButton : MonoBehaviour
  {
    public GameObject OnImage;
    public GameObject OffImage;
    private AudioManager _audioManager;
    public Slider Slider;
    public GameObject SliderHolder;

    private bool _on = true;
    public float DefaultValue { get; set; }

    protected const string MasterVolume = "";
    private float _sliderValue;
    private void Awake()
    {
      _on = true;
      _audioManager = ManagerContainer.Instance.GetInstance<AudioManager>();

      if (PlayerPrefs.HasKey("SliderValue"))
      {
        _sliderValue = SaveManager.LoadData<float>("SliderValue", 1f);
      }

      else
      {
        _sliderValue = Slider.normalizedValue;
        SaveManager.SaveData("SliderValue", _sliderValue);
      }
      Slider.value = _sliderValue;
    }

    private void Start()
    {
      UpdateMasterVolume(_sliderValue);
      _audioManager.ChangeBGMAudio(_audioManager.StartBGMName);
    }

    public void UIF_BUTTON_AudioButton()
    {
      _on = !_on;
      
      OnImage.SetActive(_on);
      OffImage.SetActive(!_on);
      SliderHolder.SetActive(_on);
      UpdateMasterVolume(_sliderValue);
    }

    public void UpdateMasterVolume(float value)
    {
      DefaultValue = Mathf.Log10(value) * 20;
      
      _audioManager.MasterMixer.SetFloat(nameof(MasterVolume), DefaultValue);
      
      _on = value > 0.001f;
      
      if (value <= 0.001f)
      {
        _audioManager.MasterMixer.SetFloat(nameof(MasterVolume), -80f);
      }
      
      OnImage.SetActive(_on);
      OffImage.SetActive(!_on);
    }

    public void UIF_SLIDER_UpdateVolume()
    {
      UpdateMasterVolume(Slider.normalizedValue);
      SaveManager.SaveData("SliderValue", Slider.normalizedValue);
      
      OnImage.SetActive(_on);
      OffImage.SetActive(!_on);
    }
  }
}
