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
    protected override void Awake(){base.Awake(); TriggerInteractCallbackWithCondition = TriggerGate;}
    #endregion

    #region Gate Configuration
    /// <summary>
    /// Triggers the Gate
    /// </summary>
    public void TriggerGate()
    {
      Animator.SetTrigger(InteractAnimationHash);
      Animator.SetBool(IsOpen, !Animator.GetBool(IsOpen));
    }
  
    /// <summary>
    /// Triggers the Gate
    /// </summary>
    /// <param name="condition"></param>
    public void TriggerGate(bool condition)
    {
      Animator.SetTrigger(InteractAnimationHash);
      Animator.SetBool(IsOpen, condition);
    }
    #endregion

  }
}
