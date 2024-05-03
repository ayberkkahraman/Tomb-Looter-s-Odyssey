using JetBrains.Annotations;

namespace Project._Scripts.Runtime.Entity.EntitySystem.Entities
{
  public class EntityProperty
  {
    public delegate void OnTakeDamage(int damage);
    public delegate void OnAttack(LivingEntity entity);
    public delegate void OnDie();
    [CanBeNull] public OnTakeDamage OnTakeDamageHandler{get; set;}
    [CanBeNull] public OnAttack OnAttackHandler{get; set;}
    [CanBeNull] public OnDie OnDieHandler{get; set;}

    public EntityProperty([CanBeNull] OnTakeDamage takeDamage, [CanBeNull] OnAttack attack, [CanBeNull] OnDie die)
    {
      Subscribe(takeDamage, attack, die);
    }
        
    public void Subscribe([CanBeNull] OnTakeDamage takeDamage, [CanBeNull] OnAttack attack, [CanBeNull] OnDie die)
    {
      OnTakeDamageHandler += takeDamage;
      OnAttackHandler += attack;
      OnDieHandler += die;
    }

    public void UnSubscribe([CanBeNull] OnTakeDamage takeDamage, [CanBeNull] OnAttack attack, [CanBeNull] OnDie die)
    {
      OnTakeDamageHandler -= takeDamage;
      OnAttackHandler -= attack;
      OnDieHandler -= die;
    }
  }
}
