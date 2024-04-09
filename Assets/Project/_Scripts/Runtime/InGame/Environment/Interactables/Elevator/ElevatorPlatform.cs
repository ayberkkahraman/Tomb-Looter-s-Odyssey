using DG.Tweening;
using Project._Scripts.Runtime.InGame.Environment.Platform;
using UnityEngine;
namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Elevator
{
    public class ElevatorPlatform : ParentableObject
    {

        public ElevatorButton Button { get; set; }
        private Animator _animator;
        [Space]
        [Range(0f,5f)]public float ElevatorSpeed = 3f;
        public AnimationCurve Ease;
        [Space]
        [Space]
        public Transform DownPoint;
        public Transform TopPoint;

        private bool _isMoving;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public Vector3 TargetPosition { get; set; }

        public delegate void OnButtonTriggered();
        public OnButtonTriggered OnButtonTriggeredHandler;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            TargetPosition = transform.position;
        }

        private void OnEnable()
        {
            OnButtonTriggeredHandler += MoveElevatorPlatform;
        }

        private void OnDisable()
        {
            OnButtonTriggeredHandler -= MoveElevatorPlatform;
        }

        public void MoveElevatorPlatform()
        {
            _isMoving = true;
            _animator.SetBool(IsMoving, _isMoving);

            transform.DOMove(TargetPosition, ElevatorSpeed)
                .SetUpdate(UpdateType.Fixed)
                .SetEase(Ease).SetSpeedBased(true)
                .OnStart(() => Button.StartInteraction())
                .OnComplete(() =>
                {
                    _isMoving = false;
                    Button.EndInteraction();
                });
        }
    }
}
