using Project._Scripts.Runtime.Library.Extensions;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Collectibles
{
  public abstract class CollectableBase : MonoBehaviour
  {
    #region Fields
    public string AudioName = $"Collect";
    #endregion
    
    #region Delegates
    public delegate void OnCollectableCollected();
    public OnCollectableCollected OnCollectableCollectedHandler;
    #endregion

    #region Abstraction
    protected abstract void OnCollected();
    #endregion

    #region Unity Events
    protected virtual void OnEnable() => Init();
    protected virtual void OnDisable() => DeInit();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag($"Player")) return;

      OnCollectableCollectedHandler();
    }
    #endregion
    
    #region Initialization / DeInitialization
    protected virtual void Init()
    {
      OnCollectableCollectedHandler += DestroyCollectable;
      OnCollectableCollectedHandler += OnCollected;
      OnCollectableCollectedHandler += PlayAudioOnCollect;
    }
    protected virtual void DeInit()
    {
      OnCollectableCollectedHandler -= DestroyCollectable;
      OnCollectableCollectedHandler -= OnCollected;
      OnCollectableCollectedHandler -= PlayAudioOnCollect;
    }
    #endregion

    #region Collectable Configuration
    /// <summary>
    /// Plays the Audio when tha Player triggers with the Collectable Item
    /// </summary>
    protected virtual void PlayAudioOnCollect() => ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio(AudioName);

    /// <summary>
    /// Destroys the Collectable Item
    /// </summary>
    private void DestroyCollectable() => Destroy(gameObject);

    #endregion
  }
}
