using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics
{
  public class Gate : InteractableBase
  {
    #region Fields
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    public enum State{Open, Close}
    public State GateState { get; set; }
    #endregion


    #region Unity Functions
    public void Start()
    {
      TriggerInteractCallback = TriggerGate;
    }
    private void Update()
    {
      if(!IsInteractable) return;

      if (InputController.Interact().HasInputTriggered()) TriggerInteractCallback?.Invoke();
    }
    #endregion

    #region Gate Configuration
    /// <summary>
    /// Triggers the Gate
    /// </summary>
    public void TriggerGate()
    {
      Animator.SetTrigger(Interact);
      Animator.SetBool(IsOpen, !Animator.GetBool(IsOpen));
    }
    #endregion

  }
}
