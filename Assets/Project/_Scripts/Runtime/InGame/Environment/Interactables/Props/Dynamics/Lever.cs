using System.Collections.Generic;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Library.Controller;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics
{
  public class Lever : InteractableBase
  {
    #region Fields
    public List<Gate> TargetGates = new();
    public bool IsInteracting { get; set; }
    #endregion


    #region Unity Functions
    public void Start()
    {
      TriggerInteractCallback = () => Animator.SetTrigger(InteractAnimationHash);
    }

    private void Update()
    {
      if(!IsInteractable) return;

      if(IsInteracting) return;

      if (InputController.Interact().HasInputTriggered())
      {
        TriggerInteractCallback?.Invoke();
      }
    }
    #endregion

    #region Lever Configuration
    /// <summary>
    /// Triggers interactions of the gates
    /// </summary>
    public void TriggerGates() => TargetGates.ForEach(x => x.TriggerInteractCallback?.Invoke());
    #endregion

    #region Animation Events
    public void ANIM_EVENT_LeverActivated() => IsInteracting = true;
    public void ANIM_EVENT_LeverDeActivated() => IsInteracting = false;
    public void ANIM_EVENT_TriggerGates() => TriggerGates();
    #endregion
  }
}
