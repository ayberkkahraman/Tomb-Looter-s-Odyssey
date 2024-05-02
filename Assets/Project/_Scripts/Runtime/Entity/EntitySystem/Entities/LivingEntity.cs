using System;
using JetBrains.Annotations;
using Project._Scripts.Runtime.Interfaces;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using Project._Scripts.Runtime.ScriptableObjects;
using UnityEngine;

namespace Project._Scripts.Runtime.Entity.EntitySystem.Entities
{
    public abstract class LivingEntity : MonoBehaviour, IAudioOwner, ICameraShaker
    {
        #region Fields
        public HealthBar HealthBar;
        public UnitData UnitData;
        public UnitAudio UnitAudio;
        public int MaxHealth { get; set; }
        public int CurrentHealth{ get; set; }
        #endregion

        #region Delegates
        public delegate void OnAttack(LivingEntity entity);
        public delegate void OnTakeDamage(int damage);
        protected delegate void OnDie();

        public OnTakeDamage OnTakeDamageHandler;
        protected OnDie OnDieHandler;
        public OnAttack OnAttackHandler;
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

        public EntityProperty EntityProperty;

        protected virtual void OnEnable()
        {
            OnTakeDamageHandler += TakeDamage;
            OnDieHandler += Die;
            OnAttackHandler += Attack;

            EntityProperty = new EntityProperty(TestMethod, GiveDamageTest, TestAttack);
            
            EntityProperty.OnTakeDamageHandler += TestMethod;
            
            EntityProperty.OnDieHandler?.Invoke();
        }

        public void GiveDamageTest(int i) => Debug.Log("Damage given...");
        public void TestAttack() => Debug.Log("Attack");
        public void TestMethod(string testValue) => Debug.Log(testValue);
        public void TestMethod(int testDamage) => Debug.Log($"Damage value is : {testDamage}");
        
        protected virtual void OnDisable()
        {
            OnTakeDamageHandler -= TakeDamage;
            OnDieHandler -= Die;
            OnAttackHandler -= Attack;
        }
        #endregion

        #region Initialization
        protected virtual void InitializeComponents()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void InitializeInterfaces()
        {
            IAudioOwner.AudioManager = ManagerContainer.Instance.GetInstance<AudioManager>();
            ICameraShaker.CameraManager = ManagerContainer.Instance.GetInstance<CameraManager>();
        }
        #endregion

        #region Entity Methods
        protected abstract void Die();
        public abstract void Attack();
        public void Attack(LivingEntity entity)
        {
            if(entity == null) return;
            
            if(entity.CurrentHealth <= 0 ) return;
            
            entity.OnTakeDamageHandler(UnitData.Damage);
            
            if(!string.IsNullOrEmpty(UnitAudio.AttackAudio))
                IAudioOwner.AudioManager.PlayAudio(UnitAudio.AttackAudio);

        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth <= 0) return;
            
            CurrentHealth -= damage;
            
            if(HealthBar != null){HealthBar.UpdateHealthBar(damage);}

            if (CurrentHealth <= 0) { OnDieHandler(); return;}
            
            IAudioOwner.AudioManager.PlayAudio(UnitAudio.TakeDamageAudio);
            
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

    #region Entity Customization
    public class EntityProperty
    {
        public delegate void OnTakeDamage(int damage);
        public delegate void OnAttack(int damage);
        public delegate void OnDie();

        [CanBeNull] public OnTakeDamage OnTakeDamageHandler;
        [CanBeNull] public OnAttack OnAttackHandler;
        [CanBeNull] public OnDie OnDieHandler;

        public EntityProperty([CanBeNull] OnTakeDamage takeDamage, [CanBeNull] OnAttack attack, [CanBeNull] OnDie die)
        {
            OnTakeDamageHandler = takeDamage;
            OnAttackHandler = attack;
            OnDieHandler = die;
        }
    }
    #endregion
}