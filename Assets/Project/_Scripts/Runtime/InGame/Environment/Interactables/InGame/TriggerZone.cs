using System.Collections.Generic;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using UnityEngine.Events;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.InGame
{
  public class TriggerZone : InteractableBase
  {
    public List<UnityEvent> Events;
    public bool DestroyAfterTriggered = true;

    private void Start()
    {
      TriggerInteractCallback = TriggerAllEvents;
    }

    public void TriggerAllEvents()
    {
      foreach (UnityEvent unityEvent in Events)
      {
        unityEvent.Invoke();
      }
      if(DestroyAfterTriggered)Destroy(gameObject);
    }
  }
}
