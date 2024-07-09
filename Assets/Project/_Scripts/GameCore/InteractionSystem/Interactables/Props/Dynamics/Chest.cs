using System.Collections;
using Project._Scripts.GameCore.InteractionSystem.Interactables.Core;
using Project._Scripts.Library.InputSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project._Scripts.GameCore.InteractionSystem.Interactables.Props.Dynamics
{
  public class Chest : InteractableBase
  {
    private Animator _animator;
    private static readonly int Trigger = Animator.StringToHash("Trigger");

    public GameObject CoinPrefab;
    public Transform CoinSpawnPosition;

    [Header("Chest Properties")]
    [Range(1, 50)]public int CoinCount = 15;
    [Range(0, 5f)]public float SpawnForce = 2.5f;
    [Range(0, .5f)]public float TimeBetweenCoins = .05f;
    private void Start()
    {
      _animator = GetComponent<Animator>();

      EndInteractCallback = () =>
      {
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
      };
    }

    private void Update()
    {
      if(!IsInteractable) return;
      
      if(InputController.Interact().HasInputTriggered()) OpenChest();
    }

    public void OpenChest()
    {
      _animator.SetTrigger(Trigger);
      GetComponent<Collider2D>().enabled = false;
      enabled = false;
    }

    IEnumerator SpawnCoins()
    {
      for (int i = 0; i < CoinCount; i++)
      {
        GameObject coin = Instantiate(CoinPrefab, CoinSpawnPosition.position, Quaternion.identity, null);
        coin.GetComponent<SpriteRenderer>().sortingOrder = i;
        Vector2 forceDirection = new Vector2(Random.Range(-SpawnForce, SpawnForce), Random.Range(SpawnForce*(2/3), SpawnForce*(20/7.5f)));
        coin.GetComponent<Rigidbody2D>().AddForce(forceDirection, ForceMode2D.Impulse);
        yield return new WaitForSeconds(TimeBetweenCoins);
      }
    }
    public void ANIM_EVENT_DropCoin()
    {
      StartCoroutine(SpawnCoins());
    }
  }
}
