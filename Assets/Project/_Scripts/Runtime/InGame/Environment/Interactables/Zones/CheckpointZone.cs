using System;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Zones
{
  public class CheckpointZone : InteractableBase
  {
    private CheckpointManager _checkpointManager;
    private bool _activated;

    private void Start()
    {
      _checkpointManager = ManagerContainer.Instance.GetInstance<CheckpointManager>();
      
      if(!PlayerPrefs.HasKey($"CheckpointZone + {gameObject.name}")) return;

      if (SaveManager.LoadData($"CheckpointZone + {gameObject.name}", true))
      {
        Destroy(gameObject);
      }
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
      _checkpointManager.UpdateCheckpointHandler(transform);
      
      SaveManager.SaveData($"CheckpointZone + {gameObject.name}", true);
      Destroy(gameObject);
    }
  }
}
