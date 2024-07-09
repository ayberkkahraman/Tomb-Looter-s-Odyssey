using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using UnityEngine;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Props.Dynamics
{
  public class Gate : InteractableBase
  {
    #region Fields
    public enum State{Open, Closed}
    public State DefaultState;
    public State CurrentState { get; set; }
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    #endregion
    
    #region Unity Functions
    protected override void Awake(){base.Awake(); TriggerInteractCallbackWithCondition = TriggerGate;}
    public void Start()
    {
      TriggerInteractCallback = TriggerGate;
      
      switch ( DefaultState )
      {
        case State.Open:
          TriggerInteractCallbackWithCondition(true);
          break;
        case State.Closed:
          TriggerInteractCallbackWithCondition(false);
          break;
      }
    }
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
      CurrentState = condition ? State.Open : State.Closed;
    }
    #endregion

  }
}
