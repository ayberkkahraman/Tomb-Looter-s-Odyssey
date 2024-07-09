using Project._Scripts.GameCore.EntitySystem.Entities.Core;
using Project._Scripts.Library.SubSystems;

namespace Project._Scripts.GameCore.EntitySystem.SubSystem
{
  public class EntityProperty
  {
    public Property<int> OnTakeDamageHandler{get; set;}
    public Property<LivingEntity> OnAttackHandler{get; set;}
    public Property<string> OnDieHandler{get; set;}
  }
}
