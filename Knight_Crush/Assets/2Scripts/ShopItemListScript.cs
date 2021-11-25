using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;
using HeroEditor.Common;

namespace Assets.Origin.Scripts
{
    public class ShopItemListScript : MonoBehaviour
    {
        public SpriteGroupEntry[] item;
        public Sprite[] ItemImg;
        public int[] Price;

        void Start()
        {

        }

        void Update()
        {
            
        }

        public SpriteGroupEntry getItem(int level)
        {
            return item[level];
        }
    }
}
