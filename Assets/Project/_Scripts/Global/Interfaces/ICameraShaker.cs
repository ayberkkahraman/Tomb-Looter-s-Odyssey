using Project._Scripts.Global.ManagerSystem.Core;
using Project._Scripts.Global.ManagerSystem.ManagerClasses;

namespace Project._Scripts.Global.Interfaces
{
  public interface ICameraShaker
  {
    public ICameraShaker CameraShaker { get; set; }
    public static CameraManager CameraManager => ManagerCore.Instance.GetInstance<CameraManager>();
    public void Shake(float amplitudeIntensity, float duration, float frequency) => CameraManager.ShakeCamera(amplitudeIntensity, duration, frequency);
  }
}