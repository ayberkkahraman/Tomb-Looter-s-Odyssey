using Project._Scripts.Runtime.Entity.EntitySystem;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Traps
{
  public class TrapSpear : TrapBase
  {
    public void ANIM_EVENT_Trigger()
    {
      if(Entity == null) return;
      
      OnTriggeredHandler(Entity);
    }

    public void ANIM_EVENT_Reset()
    {
      Animator.SetBool(Triggered, false);
    }

    protected override void Trigger(LivingEntity entity)
    {
      base.Trigger(entity);
      
      entity.OnTakeDamageHandler(1);
      
      ManagerContainer.Instance.GetInstance<CameraManager>().ShakeCamera(2f, .5f, .1f);

      float multiplier = transform.position.x - entity.transform.position.x >= 0 ? -1f : 1f;
      entity.GetComponent<Rigidbody2D>().AddForce(entity.transform.right * (multiplier * 10f), ForceMode2D.Impulse);

    }
  }
}
