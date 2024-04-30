using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics
{
  public class Gate : InteractableBase
  {
    #region Fields
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    #endregion


    #region Unity Functions
    public void Start() => TriggerInteractCallback = TriggerGate;
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
