using System;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.ScriptableObjects;
using UnityEngine;
// ReSharper disable All

namespace Project._Scripts.Runtime.Entity.EntitySystem.Entities
{
    public abstract class LivingEntity : MonoBehaviour, IDamageable, IAudioOwner, ICameraShaker
    {
        #region Fields
        public IDamageable Damageable { get; set; }
        public ICameraShaker CameraShaker { get; set; }
        public IAudioOwner AudioOwner { get; set; }

        public HealthBar HealthBar;
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
        
        protected virtual void OnEnable() => EntityProperty = new EntityProperty(TakeDamage, Attack, Die);
        protected virtual void OnDisable() => EntityProperty.UnSubscribe(TakeDamage, Attack, Die);
        #endregion

        #region Initialization
        protected virtual void InitializeComponents()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void InitializeInterfaces()
        {
            Damageable = this;
            CameraShaker = this;
            AudioOwner = this;
        }
        
        #endregion

        #region Entity Methods
        protected abstract void Die();
        public abstract void Attack();
        public void Attack(LivingEntity entity)
        {
            if(entity == null) return;
            
            if(entity.CurrentHealth <= 0 ) return;
            
            entity.EntityProperty.OnTakeDamageHandler(UnitData.Damage);
            
            if(!string.IsNullOrEmpty(UnitAudio.AttackAudio)) AudioOwner.Play(UnitAudio.AttackAudio);
        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth <= 0) return;
            
            CurrentHealth -= damage;
            
            if(HealthBar != null){HealthBar.UpdateHealthBar(damage);}

            if (CurrentHealth <= 0) { EntityProperty.OnDieHandler(); return;}
            
            AudioOwner.Play(UnitAudio.TakeDamageAudio);
            
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