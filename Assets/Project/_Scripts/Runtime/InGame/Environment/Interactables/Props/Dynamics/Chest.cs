using System;
using System.Collections;
using Project._Scripts.Runtime.InGame.Environment.Interactables.Base;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project._Scripts.Runtime.InGame.Environment.Interactables.Props.Dynamics
{
  public class Chest : InteractableBase
  {
    private Animator _animator;
    private static readonly int Trigger = Animator.StringToHash("Trigger");

    public GameObject CoinPrefab;
    public Transform CoinSpawnPosition;

    public int CoinCount;

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
        ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("CoinDrop");
        coin.GetComponent<SpriteRenderer>().sortingOrder = i;
        Vector2 forceDirection = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(5f, 20f));
        coin.GetComponent<Rigidbody2D>().AddForce(forceDirection, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.05f);
        //
      }
    }
    public void ANIM_EVENT_DropCoin()
    {
      StartCoroutine(SpawnCoins());
    }
  }
}
