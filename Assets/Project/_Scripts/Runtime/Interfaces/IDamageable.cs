namespace Project._Scripts.Runtime.Interfaces
{
  public interface IDamageable
  {
    public IDamageable Damageable { get; set; }
    public void TakeDamage(int damage);
  }
}
