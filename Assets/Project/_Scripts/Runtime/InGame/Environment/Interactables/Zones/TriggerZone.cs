using System.Collections.Generic;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using UnityEngine.Events;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Zones
{
  public class TriggerZone : InteractableBase
  {
    public List<UnityEvent> Events;
    public bool DestroyAfterTriggered = true;

    private void Start() => TriggerInteractCallback = TriggerAllEvents;

    public void TriggerAllEvents()
    {
      Events.ForEach(x => x.Invoke());

      if(DestroyAfterTriggered)Destroy(gameObject);
    }
  }
}
