using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;
using HeroEditor.Common;
using HeroEditor.Common.Enums;

namespace Assets.Origin.Scripts
{
    public class ShopScript : MonoBehaviour
    {
        public Character player;
        public ShopItemListScript Weapons;
        public GameObject Shop;
        public GameDataScript GameData;
        public MoneyScript MoneyCon;
        public HealthSliderScript HpBar;

        [Header("Images")]
        public Image FirstSlot, None;
        public Image[] Slot;
        public Image PotionSlot, none;
        public Sprite RedButton;

        [Header("Transform")]
        public Transform playerTrans;
        public Transform merchanTrans;

        [Header("Text")]
        public Text[] Price;
        public Text PotionPrice;

        public int price;
        public int level;

        bool active = false;
        // Start is called before the first frame update
        void Start()
        {
            HpBar = FindObjectOfType<HealthSliderScript>();
            GameData = FindObjectOfType<GameDataScript>();
            player = GameObject.FindWithTag("Player").GetComponent<Character>();
            playerTrans = player.gameObject.GetComponent<Transform>();
            Shop.SetActive(false);
            Slot[0].sprite = RedButton;
            Slot[1].sprite = RedButton;
            Price[1].text = 0.ToString();
            Price[2].text = 0.ToString();
            none.color = new Color(0, 0, 0, 0);
            PotionPrice.text = 80.ToString();
            level = GameData.getPlayerLevel();
            Debug.Log("level" + level);
            MoneyCon = FindObjectOfType<MoneyScript>();
        }

        // Update is called once per frame
        void Update()
        {
            float dis = Vector3.Distance(playerTrans.position, merchanTrans.position);
            if (dis <= 6.0f)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            if (active && level != 5)
            {
                Shop.SetActive(true);
                price = Weapons.Price[level];
                Price[0].text = price.ToString();
                FirstSlot.sprite = Weapons.ItemImg[level];
                None.color = new Color(0, 0, 0, 0);
            }
            else if(level == 5)
            {
                Shop.SetActive(true);
                price = Weapons.Price[level];
                Price[0].text = price.ToString();
                FirstSlot.sprite = Weapons.ItemImg[level];
                None.color = new Color(0, 0, 0, 0.3f);
            }
            else
            {
                Shop.SetActive(false);
            }
        }

        public void levelUp()
        {
            if (level >= 5)
                return;
            if (Weapons.Price[level] > GameData.OwnMoney())
                return;
            player.Equip(Weapons.item[level], EquipmentPart.MeleeWeapon1H);
            GameData.setWeapon(Weapons.getItem(level));
            MoneyCon.Use(Weapons.Price[level]);
            level++;
            GameData.setPlayerLevel(level);
        }

        public void BuyItem(int type)
        {
            
            switch (type)
            {
                case 1:
                    if (Weapons.Price[level] > GameData.OwnMoney())
                        break;
                    levelUp();
                    break;
                case 4:
                    if (80 > GameData.OwnMoney())
                        break;
                    if (GameData.getPotionCount() < 30)
                    {
                        MoneyCon.Use(80);
                        GameData.ObtainPotion();
                    }
                    break;
            }
        }
    }
}