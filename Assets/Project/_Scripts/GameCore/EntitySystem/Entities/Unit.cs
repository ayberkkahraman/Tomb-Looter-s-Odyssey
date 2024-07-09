using Project._Scripts.GameCore.EntitySystem.Entities.Core;

namespace Project._Scripts.GameCore.EntitySystem.Entities
{
    public class Unit : LivingEntity
    {
        protected override void Die()
        {
            // IAudioOwner.AudioManager.PlayAudio(UnitAudio.DeathAudio);
            Animator.SetTrigger(DeathAnimationHash);
            Animator.speed = 1f;
        }
        public override void Attack()
        {
            
        }
    }
}