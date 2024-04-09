using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
{
    public class Canon : MonoBehaviour
    {
        private Animator _anim;
        public GameObject CooldownBar { get; set; }
        public Slider CooldownSlider{ get; set; }


        [Range(0f,5f)][SerializeField]private float FireCooldown = 3f;
        [Range(0f,10f)][SerializeField]private float AttackDistance = 5f;
        public LayerMask LayersToAffect;
    
        private float _fireCooldown = 3;
        //

        public GameObject Projectile;
        public Transform ProjectileSpawnPoint;

        private int _directionMultiplier;
    
        private static readonly int Fire = Animator.StringToHash("Fire");
        private const string TargetName = "Lancelot";

        private void Start()
        {
            _anim = GetComponent<Animator>();

            CooldownBar = transform.GetChild(0).gameObject;
            CooldownSlider = CooldownBar.GetComponentInChildren<Slider>();
        
            _fireCooldown = 0f;
            _directionMultiplier = transform.rotation.y == 0 ? -1 : 1;
        }

        public void Update()
        {
            if (_fireCooldown > 0)_fireCooldown -= Time.deltaTime;
            else{ if (CanFire())FireCannon(); }
        }

        public void LateUpdate()
        {
            CooldownBar.SetActive(_fireCooldown > 0);
        
            if(!CooldownBar.activeInHierarchy) return;

            UpdateCooldownBar();
        }

        public bool CanFire()
        {
            RaycastHit2D nearbyHit2DUpper = Physics2D.Raycast(ProjectileSpawnPoint.position + new Vector3(0,.2f,0), Vector2.right * _directionMultiplier, AttackDistance, LayersToAffect);
            RaycastHit2D nearbyHit2DLower = Physics2D.Raycast(ProjectileSpawnPoint.position + new Vector3(0,-.2f,0), Vector2.right * _directionMultiplier, AttackDistance, LayersToAffect);

            return nearbyHit2DUpper.collider != null && nearbyHit2DUpper.collider.gameObject.name == TargetName &&
                   nearbyHit2DLower.collider != null && nearbyHit2DLower.collider.gameObject.name == TargetName;
        }

        private void FireCannon()
        {
            _anim.SetTrigger(Fire);
            _fireCooldown = FireCooldown;
            ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Cannon");
        }

        private void UpdateCooldownBar()
        {
            CooldownBar.SetActive(_fireCooldown > 0f);

            if(CooldownBar.activeInHierarchy)
                CooldownSlider.value = _fireCooldown / FireCooldown;
        }

        public void CreateProjectile()
        {
            Instantiate(Projectile, ProjectileSpawnPoint.position, transform.rotation, transform);
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            int directionMultiplier = transform.rotation.y == 0 ? -1 : 1;
            Gizmos.DrawLine(ProjectileSpawnPoint.position + new Vector3(0, .1f, 0), new Vector3(ProjectileSpawnPoint.transform.position.x + (directionMultiplier * AttackDistance), ProjectileSpawnPoint.position.y + .1f,0));
        }
    }
}
