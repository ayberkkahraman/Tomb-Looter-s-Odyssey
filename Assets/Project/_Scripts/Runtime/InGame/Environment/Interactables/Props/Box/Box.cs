using Project._Scripts.Runtime.InGame.Environment.Platform;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
{
  public class Box : ParentableObject
  {
    public Rigidbody2D Rigidbody2D { get; private set; }
    private void Start()
    {
      Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ApplyForce(float force, Vector2 direction)
    {
      Rigidbody2D.AddForce(direction * (force * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }

  }
}


