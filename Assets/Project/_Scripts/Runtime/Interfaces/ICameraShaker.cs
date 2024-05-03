using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;

namespace Project._Scripts.Runtime.Interfaces
{
  public interface ICameraShaker
  {
    public static CameraManager CameraManager => ManagerContainer.Instance.GetInstance<CameraManager>();
  }
}
