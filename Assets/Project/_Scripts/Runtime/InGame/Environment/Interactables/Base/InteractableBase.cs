using System;
using Project._Scripts.Runtime.CharacterController.CharacterDirectionHandler;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Library.Extensions;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Base
{
  public abstract class InteractableBase : MonoBehaviour, ITriggerable
  {
    #region Fields
    protected bool IsInteractable { get; set; }
    public bool DestroyAfterTriggerEnd;
    public bool InteractOnTrigger = true;
    
    protected static readonly int InteractAnimationHash = Animator.StringToHash("Interact");
    #endregion

    #region Actions
    public Action TriggerInteractCallback;
    public Action EndInteractCallback;
    
    public Action<bool> TriggerInteractCallbackWithCondition;
    #endregion

    #region Components
    protected Collider2D Collider2D;
    protected Animator Animator;
    protected Animator CharacterAnimator;
    protected CharacterDirection CharacterDirection;
    #endregion

    #region Unity Functions
    protected virtual void Awake() => Animator = GetComponent<Animator>();

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag($"Player")) return;

      Collider2D = other;
      
      if (!CharacterAnimator is {}) CharacterAnimator = other.GetComponent<Animator>();
      if (!CharacterDirection is {}) CharacterDirection = other.GetComponent<CharacterDirection>();

      IsInteractable = true;
      if(InteractOnTrigger)TriggerInteractCallback?.Invoke();
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag($"Player")) return;

      Collider2D = null;

      IsInteractable = false;
      
      if(InteractOnTrigger)EndInteractCallback?.Invoke();
      
      if(DestroyAfterTriggerEnd && InteractOnTrigger) Destroy(gameObject);
    }
    #endregion
  }
}