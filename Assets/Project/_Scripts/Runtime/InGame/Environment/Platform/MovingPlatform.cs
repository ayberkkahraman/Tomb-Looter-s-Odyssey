using DG.Tweening;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Platform
{
  public class MovingPlatform : MonoBehaviour
  {

    [Range(-30f, 30f)] public float TargetDistance = 5f;
    [Range(0, 10f)] public float Duration = 3f;
    [Range(0, 5f)] public float WaitTimeInTarget = 1f;

    public AnimationCurve Ease;

    public enum Axis{Horizontal, Vertical}
    public Axis MoveAxis;

    private Vector3 _targetPosition;
    private float _defaultGravityScale;

    private void Start()
    {
      switch ( MoveAxis )
      {
        case Axis.Vertical:
          transform.DOMoveY(transform.position.y + TargetDistance, Duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease)
            .SetUpdate(UpdateType.Fixed);
          break;
        case Axis.Horizontal:
          transform.DOMoveX(transform.position.x + TargetDistance, Duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease)
            .SetUpdate(UpdateType.Fixed);
          break;
      }
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.cyan;
      Gizmos.DrawLine(transform.position, transform.position);
    }
  }
}
