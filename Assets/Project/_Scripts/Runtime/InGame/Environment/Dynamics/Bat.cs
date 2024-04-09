using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project._Scripts.Runtime.InGame.Environment.Dynamics
{
  public class Bat : MonoBehaviour
  {
    [Range(0f, 20f)] public float Speed = 5f;
    private void Start()
    {
      Invoke(nameof(Reset), Random.Range(30, 45));
    }
    private void Update()
    {
      transform.position += transform.right * Speed/20;
    }

    public void Reset()
    {
      var rotationY = transform.eulerAngles.y;
      var eulerAngles = new Vector3(0, (int)rotationY == 0 ? 180 : 0, 0);
      transform.eulerAngles = eulerAngles;

      Invoke(nameof(Reset), Random.Range(30, 45));
    }
  }
}
