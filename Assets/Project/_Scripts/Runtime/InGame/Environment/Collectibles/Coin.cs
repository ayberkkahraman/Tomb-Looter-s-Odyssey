using System;
using DG.Tweening;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;
using UnityEngine;

namespace Project._Scripts.Runtime.InGame.Environment.Collectibles
{
    public class Coin : CollectableBasic
    {
        private void Start()
        {
            Invoke(nameof(SetRigidbody), .35f);
        }

        void SetRigidbody()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        protected override void OnCollected()
        {
            int coin = SaveManager.LoadData("Coin", 0);
            coin++;
            SaveManager.SaveData("Coin", coin);
            // ManagerContainer.Instance.GetInstance<UIManager>().UpdateCoinText(coin);
            Destroy(gameObject);
        }
    }
}