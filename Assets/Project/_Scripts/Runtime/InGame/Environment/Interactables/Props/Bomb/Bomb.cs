using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props
{
    public class Bomb : InteractableBase
    {
        public Rigidbody2D Rigidbody2D { get; set; }
        private Animator _animator;
        public GameObject CooldownBar{ get; set; }
        public Slider CooldownSlider{ get; set; }
        public bool IsActivated { get; set; }
        public bool Exploded { get; set; }

        [SerializeField] Collider2D[] DamageableColliders = new Collider2D[20];
        
        
        [Range(0f, 5f)][SerializeField] private float ExplodeTime = 2f;
        [Range(0f, 5f)] [SerializeField] private float ExplodeDamage = 2f;
        [Range(1f, 5f)] [SerializeField] private float ExplodeRadius = 3f;
    
        private float _explodeTimer;
    
        private bool _canAffectWarningSign;
        private bool _affectedToWarningSign;
    
        private static readonly int Activated = Animator.StringToHash("IsActivated");
        private static readonly int ExplodeAnimationHash = Animator.StringToHash("Explode");
        
        protected const string AudioBombActivated = "Bomb_Activated";
        protected const string AudioBomb = "Bomb";
        public void Start()
        {
            _animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _explodeTimer = ExplodeTime;

            CooldownBar = transform.GetChild(0).gameObject;
            CooldownSlider = CooldownBar.GetComponentInChildren<Slider>();

            TriggerInteractCallback = InteractionStart;
        }
        public void Update()
        {
            if (!IsActivated)
                return;
        
            if(Exploded) return;

            if (_explodeTimer > 0) {  _explodeTimer -= Time.deltaTime; }
            else { Explode(); }
        }

        public void LateUpdate()
        {
            if (CooldownBar.activeInHierarchy) { CooldownSlider.value = _explodeTimer / ExplodeTime; }
        }

    
        public void InteractionStart()
        {
            if(IsActivated) return;
            
            IsActivated = true;
            _animator.SetBool(Activated, IsActivated);
            CooldownBar.SetActive(true);
            ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio(AudioBombActivated);
        }

        public void Explode()
        {
            _animator.SetTrigger(ExplodeAnimationHash);
            CooldownBar.SetActive(false);
            ManagerContainer.Instance.GetInstance<CameraManager>().ShakeCamera(1.25f, .25f);
            ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio(AudioBomb);
        
            int damageableCount = Physics2D.OverlapCircleNonAlloc(transform.position, ExplodeRadius, DamageableColliders);

            for (int i = 0; i < damageableCount; i++)
            {
                var damageable = DamageableColliders[i];
                Rigidbody2D damageableRb2D = damageable.GetComponent<Rigidbody2D>();
                
                if (damageableRb2D == null)
                    continue;
            
                Vector2 direction = (damageable.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, damageable.transform.position);

                if (!IsActivated)
                    continue;
            
                float force = (ExplodeDamage) / (damageableRb2D.mass * distance);
                damageableRb2D.AddForce(direction * force/2.5f, ForceMode2D.Impulse);
            }

            Exploded = true;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, ExplodeRadius);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}

