using Project._Scripts.Global.ManagerSystem.Core;
using Project._Scripts.Global.ManagerSystem.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Effects
{
  public class ParticleEffect : MonoBehaviour
  {
    private PoolManager _poolManager;
    private void Awake()
    {
      _poolManager = ManagerCore.Instance.GetInstance<PoolManager>();
    }

    public void DestroyObject()
    {
      _poolManager.DestroyPoolObject(this);
    }
  }
}
