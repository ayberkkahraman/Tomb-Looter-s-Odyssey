using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Project._Scripts.Runtime.InGame.GameSystems
{
  public class Parallax : MonoBehaviour
  {
    private float _lenght;
    private float _startPosition;
    public GameObject Camera;
    public float ParallaxFactor;

    private void Start()
    {
      _startPosition = transform.position.x;
      _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
      float temp = (Camera.transform.position.x * (1 - ParallaxFactor));
      float dist = (Camera.transform.position.x * ParallaxFactor);

      transform.position = new Vector3(_startPosition + dist, transform.position.y, transform.position.z);

      if (temp > _startPosition + _lenght) _startPosition += _lenght;
      else if (temp < _startPosition - _lenght) _startPosition -= _lenght;
    }
  }
}
