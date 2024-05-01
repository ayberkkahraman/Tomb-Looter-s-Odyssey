using Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Collectibles
{
  public class Key : CollectableBasic
  {
    public Gate TargetGate;
    protected override void OnCollected() => TargetGate.TriggerInteractCallbackWithCondition?.Invoke(true);
  }
}
