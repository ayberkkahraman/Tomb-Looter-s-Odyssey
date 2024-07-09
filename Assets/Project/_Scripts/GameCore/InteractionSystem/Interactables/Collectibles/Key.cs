using Project._Scripts.GameCore.InteractionSystem.Interactables.Props.Dynamics;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Collectibles
{
  public class Key : CollectableBasic
  {
    public Gate TargetGate;
    protected override void OnCollected() => TargetGate.TriggerInteractCallbackWithCondition?.Invoke(true);
  }
}
