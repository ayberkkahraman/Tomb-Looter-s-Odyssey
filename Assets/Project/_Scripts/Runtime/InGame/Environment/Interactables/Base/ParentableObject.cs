using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Platform
{
  public class ParentableObject : MonoBehaviour, IParentable
  {
    public LayerMask LayersToBeGetChild;
    public bool AlreadyParented { get; set; }
    public void OnParent(Transform other)
    {
      other.SetParent(transform);
      AlreadyParented = true;
    }

    public void OnUnParent(Transform other)
    {
      other.SetParent(null);
      AlreadyParented = false;
    }

    public string[] GetLayerNamesFromMask(LayerMask layerMask)
    {
      List<string> layerNames = new List<string>();

      for (int i = 0; i < 32; i++)
      {
        if (((1 << i) & layerMask.value) == 0)
          continue;
        
        string layerName = LayerMask.LayerToName(i);
        layerNames.Add(layerName);
      }

      return layerNames.ToArray();
    }
    private bool IsInTargetLayer(Component component)
    {
      var layers = GetLayerNamesFromMask(LayersToBeGetChild).ToList();
      var layer = LayerMask.LayerToName(component.gameObject.layer);
      return layers.Contains(layer);
    }

    // Handle the collision with other objects
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(AlreadyParented) return;

      if(!IsInTargetLayer(other)) return;
      OnParent(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if(!AlreadyParented) return;

      if(!IsInTargetLayer(other)) return;
      OnUnParent(other.transform);
    }
  }

  public interface IParentable
  {
    void OnParent(Transform parent);
    void OnUnParent(Transform parent);
  }
}