using System.Collections;
using Project._Scripts.Global.Interfaces;
using UnityEngine;

namespace Project._Scripts.GameCore.HazardousSystem.Hazardouses.Core
{
  public abstract class HazardousBase : MonoBehaviour
  {
    [Range(0, 5)] public int Damage = 1;
    [Range(0f, 5f)] public float AttackInterval = 3f;

    protected Animator Animator;
    protected IDamageable Damageable;

    private static readonly int Trigger = Animator.StringToHash("Trigger");

    public abstract void ANIM_EVENT_GiveDamage();

    protected virtual void Awake()
    {
      Animator = GetComponent<Animator>();
    }

    protected IEnumerator Start()
    {
      while (true)
      {
        yield return new WaitForSeconds(AttackInterval);
        Animator.SetTrigger(Trigger);
      }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.layer != LayerMask.NameToLayer($"Player")) return;

      other.TryGetComponent(out IDamageable damageable);
      
      Damageable = damageable;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
      Damageable = null;
    }

  }
}
