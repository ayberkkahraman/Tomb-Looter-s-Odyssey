// using UnityEngine;
//
// namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
// {
//     public class PushPlatform : MonoBehaviour
//     {
//         public PlatformGate gate;
//
//         SpriteRenderer sr;
//
//         public Sprite activeSprite;
//         public Sprite inactiveSprite;
//
//         public Transform rightPointTransform;
//         public Transform leftPointTransform;
//
//         public float boxCheckerDistance;
//
//         public bool isActive;
//
//         private bool activated;
//
//         public void Start()
//         {
//             sr = GetComponent<SpriteRenderer>();
//         }
//
//         public void Update()
//         {
//             // isActive = IsBoxOnTop();
//
//             sr.sprite = isActive ? activeSprite : inactiveSprite;
//
//             if(!activated && isActive)
//             {
//                 gate.anim.SetTrigger("Trigger");
//                 gate.OpenGate();
//                 activated = true;
//             }
//
//             if (!isActive && activated)
//             {
//                 gate.anim.SetTrigger("Trigger");
//                 gate.CloseGate();
//                 activated = false;
//             }
//         }
//
//         // public bool IsBoxOnTop()
//         // {
//         //     RaycastHit2D[] _rightRaycasts = Physics2D.RaycastAll(rightPointTransform.position, Vector2.up, boxCheckerDistance);
//         //     RaycastHit2D[] _leftRaycasts = Physics2D.RaycastAll(leftPointTransform.position, Vector2.up, boxCheckerDistance);
//         //
//         //     return _rightRaycasts.ToList().Exists(x => x.collider.GetComponent<MoveEffector>() != null || x.collider.GetComponent<Lancelot>() != null) ||
//         //            _leftRaycasts.ToList().Exists(x => x.collider.GetComponent<MoveEffector>() != null || x.collider.GetComponent<Lancelot>() != null);
//         // }
//
//         public void OnDrawGizmosSelected()
//         {
//             Gizmos.color = Color.yellow;
//
//             Gizmos.DrawLine(rightPointTransform.position, new Vector3(rightPointTransform.position.x, rightPointTransform.position.y + boxCheckerDistance, 0));
//             Gizmos.DrawLine(leftPointTransform.position, new Vector3(leftPointTransform.position.x, leftPointTransform.position.y + boxCheckerDistance, 0));
//         }
//     }
// }
