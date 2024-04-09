using System.Linq;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.BinocularVision
{
    public class MiniCamera : MonoBehaviour
    {
        [Range(1f,10f)]public float InspectVelocity = 7.5f;

        public GameObject BinocularVision;
        private Vector2 _moveDirection;

        private CameraManager _cameraManager;
        private Animator _binocularVisionAnimator;
        public bool IsActive { get; set; }

        [HideInInspector] public Tilemap Tilemap;

        [Header("Limits")]
        private float _xLimitPositive;
        private float _xLimitNegative;
        private float _yLimitPositive;
        private float _yLimitNegative;
        private static readonly int CloseAnimationHash = Animator.StringToHash("Close");
        private static readonly int OpenAnimationHash = Animator.StringToHash("Open");

        public delegate void OnMiniCameraOpened();
        public delegate void OnMiniCameraClosed();

        public OnMiniCameraOpened OnMiniCameraOpenedHandler => Open;
        public OnMiniCameraClosed OnMiniCameraClosedHandler => Close;

        private void Awake()
        {
            InitializeComponents();
            InitializeTilemap();
            InitializeCamera();
        }

        public void LateUpdate()
        {        
            CalculatePosition();
        }

        private void InitializeComponents()
        {
            _cameraManager = ManagerContainer.Instance.GetInstance<CameraManager>();
            ManagerContainer.Instance.GetInstance<UIManager>();

            _binocularVisionAnimator = BinocularVision.GetComponent<Animator>();
        }

        private void InitializeTilemap()
        {
            Tilemap = FindObjectsOfType<TilemapRenderer>().ToList().Find(x => x.gameObject.name.Contains("ForeGround")).GetComponent<Tilemap>();

            Tilemap.CompressBounds();
        }

        private void InitializeCamera()
        {
            var cameraSizeDiff = GetComponent<Camera>().orthographicSize;

            _xLimitPositive = Tilemap.transform.TransformPoint(Tilemap.cellBounds.max).x - cameraSizeDiff - 2;
            _yLimitPositive = Tilemap.transform.TransformPoint(Tilemap.cellBounds.max).y - cameraSizeDiff;
            _xLimitNegative = Tilemap.transform.TransformPoint(Tilemap.cellBounds.min).x + cameraSizeDiff + 2;
            _yLimitNegative = Tilemap.transform.TransformPoint(Tilemap.cellBounds.min).y + cameraSizeDiff;
        }
        public void CalculatePosition()
        {
            _moveDirection = InputController.ControllerInput.CharacterController.Move.ReadValue<Vector2>();
            transform.Translate(_moveDirection * (Time.deltaTime * InspectVelocity));

            transform.position = GetBoundPosition();
        }

        private Vector3 GetBoundPosition()
        {
            return new Vector3(
                Mathf.Clamp(transform.position.x, _xLimitNegative, _xLimitPositive),
                Mathf.Clamp(transform.position.y, _yLimitNegative, _yLimitPositive),
                transform.position.z);
        }

        public void Open()
        {
            IsActive = true;
        
            BinocularVision.gameObject.SetActive(true);
            
            _binocularVisionAnimator.SetTrigger(OpenAnimationHash);
        
            _binocularVisionAnimator.ResetTrigger(CloseAnimationHash);

            transform.position = _cameraManager.MainCamera.transform.position;
        }

        public void Close()
        {
            IsActive = false;

            _binocularVisionAnimator.SetTrigger(CloseAnimationHash);
            
            _binocularVisionAnimator.ResetTrigger(OpenAnimationHash);
        }
    }
}
