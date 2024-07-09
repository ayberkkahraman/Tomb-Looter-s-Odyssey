namespace Project._Scripts.Global.Interfaces
{
  public interface IDamageable
  {
    public IDamageable Damageable { get; set; }
    public void TakeDamage(int damage);
  }
}
