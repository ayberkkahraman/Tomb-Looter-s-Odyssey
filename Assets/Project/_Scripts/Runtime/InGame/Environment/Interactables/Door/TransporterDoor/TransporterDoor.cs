using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Library.Controller;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Door
{
  public class TransporterDoor : InteractableBase
  {
    public TransporterDoor TargetDoor;
    public Transform TransportTransform { get; set; }
    
    private void Start()
    {
      TransportTransform = transform.GetChild(0);
    }
  }
}
