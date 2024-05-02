﻿using Project._Scripts.Runtime.Interfaces;

namespace Project._Scripts.Runtime.Entity.EntitySystem.Entities
{
    public class Unit : LivingEntity
    {
        protected override void Die()
        {
            IAudioOwner.AudioManager.PlayAudio(UnitAudio.DeathAudio);
            Animator.SetTrigger(DeathAnimationHash);
            Animator.speed = 1f;
        }
        public override void Attack()
        {
            
        }
    }
}