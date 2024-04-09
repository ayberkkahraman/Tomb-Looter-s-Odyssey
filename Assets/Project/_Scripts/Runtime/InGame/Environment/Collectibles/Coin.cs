using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;

namespace Project._Scripts.Runtime.InGame.Environment.Collectibles
{
    public class Coin : CollectableBasic
    {
        protected override void OnCollected()
        {
            int coin = SaveManager.LoadData("Coin", 0);
            coin++;
            SaveManager.SaveData("Coin", coin);
            ManagerContainer.Instance.GetInstance<UIManager>().UpdateCoinText(coin);
            Destroy(gameObject);
        }
    }
}