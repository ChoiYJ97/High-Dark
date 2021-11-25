using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class PlayerStatus : MonoBehaviour
    {
        public GameDataScript GameData;
        public int TotalPower;
        public int PowerWeapon;
        public int PowerArmor;
        public int Damage;
        public int Hp;
        public int Critical;
        public int Defense;

        public int weaponLevel;
        public int armorLevel;


        public LoadLevelDataScript LevelData;

        void Start()
        {
            GameData = FindObjectOfType<GameDataScript>();
            weaponLevel = GameData.getPlayerLevel();
            armorLevel = GameData.getPlayerLevel();
            LevelData = FindObjectOfType<LoadLevelDataScript>();

            WeaponUpdate(weaponLevel);

            ArmorUpdate(armorLevel);
        }

        private void Update()
        {
            weaponLevel = GameData.getPlayerLevel();
            armorLevel = GameData.getPlayerLevel();
            WeaponUpdate(weaponLevel);

            ArmorUpdate(armorLevel);
        }

        public void WeaponUpdate(int WpLevel)
        {
            weaponLevel = WpLevel;

            PowerWeapon = (int)LevelData.getValue(0, weaponLevel);
            Damage = (int)LevelData.getValue(1, weaponLevel);
            Critical = (int)LevelData.getValue(3, weaponLevel);

            TotalPower = PowerArmor + PowerWeapon;
        }

        public void ArmorUpdate(int AmLevel)
        {
            armorLevel = AmLevel;

            PowerArmor = (int)LevelData.getValue(0, weaponLevel);
            Hp = (int)LevelData.getValue(2, weaponLevel);
            Defense = (int)LevelData.getValue(4, weaponLevel);

            TotalPower = PowerArmor + PowerWeapon;
        }

        public int Power(int type)
        {
            switch (type)
            {
                case 0:
                    return PowerArmor;
                case 1:
                    return PowerWeapon;
                default:
                    return PowerArmor + PowerWeapon;
            }
        }

        public int getStatus(int type)
        {
            switch (type)
            {
                case 0:
                    return Damage;
                case 1:
                    return Critical;
                case 2:
                    return Hp;
                case 3:
                    return Defense;
                default:
                    return 0;
            }
        }

        public int getLevel(int Type)
        {
            switch (Type)
            {
                case 1:
                    return weaponLevel;
                case 2:
                    return armorLevel;
                default:
                    return 0;
            }
        }
    }
}
