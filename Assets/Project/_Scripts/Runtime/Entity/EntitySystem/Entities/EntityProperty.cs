using Project._Scripts.Runtime.Library.SubSystems;

namespace Project._Scripts.Runtime.Entity.EntitySystem.Entities
{
  public class EntityProperty
  {
    public Property<int> OnTakeDamageHandler{get; set;}
    public Property<LivingEntity> OnAttackHandler{get; set;}
    public Property<string> OnDieHandler{get; set;}
  }
}
