using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Zones
{
  public class CheckpointZone : InteractableBase
  {
    // private CheckpointManager _checkpointManager;
    private bool _activated;

    private void Start()
    {
      // _checkpointManager = ManagerCore.Instance.GetInstance<CheckpointManager>();
    }

    private void OnEnable()
    {
      TriggerInteractCallback += SetCheckpoint;
    }

    private void OnDisable()
    {
      TriggerInteractCallback -= SetCheckpoint;
    }

    public void SetCheckpoint()
    {
      // _checkpointManager.UpdateCheckpointHandler(transform);
      Destroy(gameObject);
    }
  }
}
