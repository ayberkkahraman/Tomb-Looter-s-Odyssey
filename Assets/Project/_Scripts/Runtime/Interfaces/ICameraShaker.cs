using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;

namespace Project._Scripts.Runtime.Interfaces
{
  public interface ICameraShaker
  {
    public ICameraShaker CameraShaker { get; set; }
    public static CameraManager CameraManager => ManagerContainer.Instance.GetInstance<CameraManager>();
    public void Shake(float amplitudeIntensity, float duration, float frequency) => CameraManager.ShakeCamera(amplitudeIntensity, duration, frequency);
  }
}