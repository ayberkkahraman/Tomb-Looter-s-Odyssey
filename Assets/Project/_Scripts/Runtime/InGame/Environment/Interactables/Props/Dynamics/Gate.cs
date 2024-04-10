using System;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics
{
  public class Gate : InteractableBase
  {
    private static readonly int Interact = Animator.StringToHash("Interact");

    public void Start()
    {
      TriggerInteractCallback = () => { GetComponent<Animator>().SetTrigger(Interact);};
    }
    private void Update()
    {
      if(!IsInteractable) return;

      if (InputController.Interact().HasInputTriggered())
      {
        TriggerInteractCallback?.Invoke();
      }
    }
  }
}
