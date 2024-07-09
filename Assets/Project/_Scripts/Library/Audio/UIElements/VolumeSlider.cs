using Project._Scripts.Library.Audio.UIElements.Core;
using Project._Scripts.Library.Configuration.Progress;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Project._Scripts.Library.Audio.UIElements
{
  [DefaultExecutionOrder(960)]
  public class VolumeSlider : MonoBehaviour
  {
    public enum Type { MasterVolume, SfxVolume, MusicVolume };
    public Type VolumeType;
    
    public AudioMixer AudioMixer;
    public Slider Slider { get; set; }
    private void Awake() => InitiliazeSlider();
    private void OnEnable() => LoadSlider();
    private void OnDisable() => SaveSlider();
    private void Start() => SLIDER_OnChange(Slider.value);
    public void SLIDER_OnChange(float value) => AudioMixer.SetMixerValue($"{VolumeType.ToString()}", Core.Core.TrueVolume(value));
    public void SaveSlider() => Progress.Save($"{VolumeType.ToString()}", Slider.value);
    public void LoadSlider() => Slider.value = Progress.Load($"{VolumeType.ToString()}", 1f);
    public void InitiliazeSlider()
    {
      Slider = GetComponentInChildren<Slider>();
      var sliderEvent = new Slider.SliderEvent();
      sliderEvent.AddListener((SLIDER_OnChange));
      Slider.onValueChanged = sliderEvent;
    }
  }
}
