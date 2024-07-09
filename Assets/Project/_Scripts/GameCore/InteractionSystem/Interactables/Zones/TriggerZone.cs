using System.Collections.Generic;
using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using UnityEngine.Events;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Zones
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
