using UnityEngine;

namespace Project._Scripts.Runtime.Interfaces
{
  public interface ITriggerable
  {
    public void OnTriggerEnter2D(Collider2D other);
    public void OnTriggerExit2D(Collider2D other);
  }
}
