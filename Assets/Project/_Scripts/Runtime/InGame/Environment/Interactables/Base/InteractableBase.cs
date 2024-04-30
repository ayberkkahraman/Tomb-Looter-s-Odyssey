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
    protected Animator Animator;
    protected Animator CharacterAnimator;
    protected CharacterController.CharacterController.CharacterController CharacterController;

    public bool DestroyAfterTriggerEnd;
    public bool InteractOnTrigger = true;
    
    protected static readonly int Interact = Animator.StringToHash("Interact");

    protected void Awake()
    {
      Animator = GetComponent<Animator>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag("Player")) return;

      Collider2D = other;
      if (CharacterAnimator == null) CharacterAnimator = other.GetComponent<Animator>();
      if (CharacterController == null) CharacterController = other.GetComponent<CharacterController.CharacterController.CharacterController>();

      IsInteractable = true;
      if(InteractOnTrigger)TriggerInteractCallback?.Invoke();
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
      if(!other.gameObject.CompareTag("Player")) return;

      Collider2D = null;

      IsInteractable = false;
      if(InteractOnTrigger)EndInteractCallback?.Invoke();
      
      if(DestroyAfterTriggerEnd && InteractOnTrigger) Destroy(gameObject);
    }
  }
}