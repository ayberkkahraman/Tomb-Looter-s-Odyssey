using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.Effects
{
  public class ParticleEffect : MonoBehaviour
  {
    private PoolManager _poolManager;
    private void Awake()
    {
      _poolManager = ManagerContainer.Instance.GetInstance<PoolManager>();
    }

    public void DestroyObject()
    {
      _poolManager.DestroyPoolObject(this);
    }
  }
}
