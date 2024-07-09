using System;
using Project._Scripts.GameCore.EntitySystem.SubSystem;
using Project._Scripts.Global.Interfaces;
using Project._Scripts.Library.SubSystems;
using Project._Scripts.ScriptableObjects;
using UnityEngine;

namespace Project._Scripts.GameCore.EntitySystem.Entities.Core
{
    public abstract class LivingEntity : MonoBehaviour, IDamageable, ICameraShaker
    {
        #region Fields
        public IDamageable Damageable { get; set; }
        public ICameraShaker CameraShaker { get; set; }

        public HealthBar.HealthBar HealthBar;
        public UnitData UnitData;
        public UnitAudio UnitAudio;
        public int MaxHealth { get; set; }
        public int CurrentHealth{ get; set; }
        #endregion

        #region Entity Property
        public EntityProperty EntityProperty;
        #endregion

        #region Animation Hash Parameters
        protected static readonly int TakeHitAnimationHash = Animator.StringToHash("TakeHit");
        protected static readonly int DeathAnimationHash = Animator.StringToHash("Death");
        #endregion

        #region Components
        protected Animator Animator { get; set; }
        #endregion

        #region Unity Functions
        protected virtual void Awake()
        {
            InitializeComponents();
            InitializeInterfaces();
        }
        
        protected virtual void Start()
        {
            MaxHealth = UnitData.Health;
            CurrentHealth = MaxHealth;
        }

        protected virtual void OnEnable() => InitializeEntityProperty();
        protected virtual void OnDisable() => DeInitializeEntityProperty();
        #endregion

        #region Initialization / DeInitialization
        protected virtual void InitializeComponents()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void InitializeInterfaces()
        {
            Damageable = this;
            CameraShaker = this;
        }

        protected virtual void InitializeEntityProperty()
        {
            EntityProperty = new EntityProperty
            {
                OnDieHandler = new Property<string>(_ => Die()),
                OnTakeDamageHandler = new Property<int>(TakeDamage),
                OnAttackHandler = new Property<LivingEntity>(Attack)
            };
        }
        
        protected virtual void DeInitializeEntityProperty()
        {
            EntityProperty.OnDieHandler.UnSubscribe(_ => Die());
            EntityProperty.OnTakeDamageHandler.UnSubscribe(TakeDamage);
            EntityProperty.OnAttackHandler.UnSubscribe(Attack);
        }
        
        #endregion

        #region Entity Methods
        protected abstract void Die();
        public abstract void Attack();
        public void Attack(LivingEntity entity)
        {
            if(entity == null) return;
            
            if(entity.CurrentHealth <= 0 ) return;
            
            entity.EntityProperty.OnTakeDamageHandler.Invoke(UnitData.Damage);
            
            // if(!string.IsNullOrEmpty(UnitAudio.AttackAudio)) AudioOwner.Play(UnitAudio.AttackAudio);
        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth <= 0) return;
            
            CurrentHealth -= damage;
            
            if(HealthBar != null){HealthBar.UpdateHealthBar(damage);}

            if (CurrentHealth <= 0) { EntityProperty.OnDieHandler.Invoke(); return;}
            
            // AudioOwner.Play(UnitAudio.TakeDamageAudio);
            
            Animator.SetTrigger(TakeHitAnimationHash);
        }
        #endregion
    }

    #region Entity Audio Class
    [Serializable]
    public struct UnitAudio
    {
        public string AttackAudio;
        public string TakeDamageAudio;
        public string DeathAudio;
    }
    #endregion
}