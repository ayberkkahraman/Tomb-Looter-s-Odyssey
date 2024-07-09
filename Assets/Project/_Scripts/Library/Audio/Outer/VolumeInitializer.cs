using System.Collections.Generic;
using System.Linq;
using Project._Scripts.Library.Audio.UIElements;
using UnityEngine;

namespace Project._Scripts.Library.Audio.Outer
{
  [DefaultExecutionOrder(620)]
  public class VolumeInitializer : MonoBehaviour
  {
    public List<VolumeSlider> VolumeSliders { get; set; }

    private void Awake()
    {
      VolumeSliders = new List<VolumeSlider>();
      VolumeSliders = GetComponentsInChildren<VolumeSlider>(true).ToList();
      VolumeSliders.ForEach(x =>
      {
        x.InitiliazeSlider();
        x.LoadSlider();
        x.SLIDER_OnChange(x.Slider.value);
      });
    }
  }
}
