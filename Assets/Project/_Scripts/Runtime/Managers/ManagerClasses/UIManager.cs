using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project._Scripts.Runtime.Managers.ManagerClasses
{
    public class UIManager : MonoBehaviour
    {
        [Header("Dialogue")]
        public GameObject DialogueBox;
        public TMP_Text DialogueText;
        public TMP_Text DialogueNameText;
        public TMP_Text CoinCount;
        public Image DialogueImage;

        private void Start()
        {
            if(CoinCount.gameObject.activeInHierarchy)
                CoinCount.text = SaveManager.LoadData("Coin", 0).ToString();
        }
        
        public void UpdateCoinText(int coinCount)
        {
            CoinCount.text = coinCount.ToString();
        }
        
        public void LoadCoinText()
        {
            CoinCount.text = SaveManager.LoadData("Coin", 0).ToString();
        }

        public void UpdateDialogueText(string text)
        {
            DialogueText.text = text;
        }
        
        public void UpdateDialogueNameText(string text)
        {
            DialogueNameText.text = text;
        }

        public void UpdateDialogueImage(Sprite sprite)
        {
            DialogueImage.sprite = sprite;
        }

    }
}
