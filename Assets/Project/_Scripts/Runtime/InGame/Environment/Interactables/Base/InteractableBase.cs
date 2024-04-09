using System;
using Project._Scripts.Runtime.Interfaces;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Base
{
  public abstract class InteractableBase : MonoBehaviour, ITriggerable
  {
    protected bool IsInteractable { get; set; }

    public Action TriggerInteractCallback;
    public Action EndInteractCallback;

    protected Collider2D Collider2D;

    public bool DestroyAfterTriggerEnd;
    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag("Player")) return;

      Collider2D = other;

      IsInteractable = true;
      TriggerInteractCallback?.Invoke();
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag("Player")) return;

      Collider2D = null;

      IsInteractable = false;
      EndInteractCallback?.Invoke();
      
      if(DestroyAfterTriggerEnd) Destroy(gameObject);
    }
  }
}