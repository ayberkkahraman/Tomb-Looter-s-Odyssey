﻿using Project._Scripts.Runtime.Entity.EntitySystem.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.Entity.EntitySystem
{
    public class HealthBarBase : MonoBehaviour
    {
        public Image Bar;
        public Unit Unit { get; set; }

        protected virtual void Start()
        {
            Unit = GetComponentInParent<Unit>();
            Unit.EntityProperty.OnTakeDamageHandler.Subscribe(UpdateHealthBar);
        }

        protected virtual  void LateUpdate()
        {
            transform.localRotation = transform.parent.transform.rotation;
        }

        public virtual  void UpdateHealthBar(int health)
        {
            Bar.fillAmount = Unit.CurrentHealth / Unit.MaxHealth;
            if(Unit.CurrentHealth <= 0) gameObject.SetActive(false);
        }
    }
}