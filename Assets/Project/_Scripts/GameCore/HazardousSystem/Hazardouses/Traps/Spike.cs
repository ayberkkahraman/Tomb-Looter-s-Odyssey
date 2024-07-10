using Project._Scripts.GameCore.HazardousSystem.Hazardouses.Core;
using UnityEngine;

namespace Project._Scripts.GameCore.HazardousSystem.Hazardouses.Traps
{
  public class Spike : HazardousBase
  {
    public override void ANIM_EVENT_GiveDamage()
    {
      if(Damageable is null) return;
      
      Damageable.TakeDamage(Damage);
    }
  }
}
