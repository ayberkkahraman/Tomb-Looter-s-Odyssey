using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Collectibles
{
  public abstract class CollectableBase : MonoBehaviour
  {
    public delegate void OnCollectableCollected();
    public OnCollectableCollected OnCollectableCollectedHandler;

    protected abstract void OnCollected();

    protected virtual void OnEnable()
    {
      OnCollectableCollectedHandler += OnCollected;
      OnCollectableCollectedHandler += DefaultCollectHandler;
    }
    
    protected virtual void OnDisable()
    {
      OnCollectableCollectedHandler -= OnCollected;
      OnCollectableCollectedHandler -= DefaultCollectHandler;
    }

    protected virtual void DefaultCollectHandler()
    {
      ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Collect");
      Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.layer != LayerMask.NameToLayer($"Player")) return;

      OnCollectableCollectedHandler?.Invoke();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
      if(other.gameObject.layer != LayerMask.NameToLayer($"Player")) return;

      OnCollectableCollectedHandler?.Invoke();
    }
  }
}
