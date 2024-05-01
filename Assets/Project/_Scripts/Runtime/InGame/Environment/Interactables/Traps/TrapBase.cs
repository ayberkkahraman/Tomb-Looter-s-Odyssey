using Project._Scripts.Runtime.Entity.EntitySystem;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Traps
{
  public class TrapBase : InteractableBase
  {
    protected LivingEntity Entity;

    [Range(0f,5f)]public float Cooldown = 2f;
    public float CurrentCooldown { get; set; }

    public delegate void OnTriggered(LivingEntity entity);
    public OnTriggered OnTriggeredHandler;
    protected static readonly int Triggered = Animator.StringToHash("Triggered");

    private void Start()
    {
      Animator = GetComponent<Animator>();
      CurrentCooldown = Cooldown;

      TriggerInteractCallback = () =>
      {
        Entity = Collider2D.GetComponent<LivingEntity>();
        Animator.SetBool(Triggered, true);
      };

      EndInteractCallback = () =>
      {
        Entity = null;
        Animator.SetBool(Triggered, false);
      };
    }

    private void OnEnable()
    {
      OnTriggeredHandler += Trigger;
    }

    private void OnDisable()
    {
      OnTriggeredHandler -= Trigger;
    }

    private void Update()
    {
      if (CurrentCooldown < Cooldown) CurrentCooldown += Time.deltaTime;

      if (!IsInteractable)
        return;

      if (!(CurrentCooldown >= Cooldown))
        return;
      
      TriggerInteractCallback?.Invoke();
      CurrentCooldown = 0;
    }


    protected virtual void Trigger(LivingEntity entity)
    {
      // ReSharper disable once RedundantJumpStatement
      if(Entity.Health <= 0) return;
    }
  }
}
