using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class LoadLevelDataScript : MonoBehaviour
    {
        [Header("Text Files")]
        public TextAsset MobLevelDataText;
        public TextAsset LevelDataText;
        public TextAsset DropDataText;

        public LevelDesignData level_data;
        public int CurLevel = 0;

        private void Start()
        {
            this.level_data = FindObjectOfType<LevelDesignData>();
            DontDestroyOnLoad(gameObject);
            this.level_data.initialize();
            this.level_data.LoadLevelData(this.LevelDataText);
            this.level_data.Load_Mon_LevelData(this.MobLevelDataText);
            this.level_data.Load_Mob_DropData(this.DropDataText);
            getAllStatusValue();
        }

        public void getAllStatusValue()
        {
            this.level_data.currentLevel(CurLevel);
            for (int i = 0; i < 5; i++)
            {
                Debug.Log(getValue(i, CurLevel));
            }

            this.level_data.currentMobLevel(CurLevel);
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(getMobValue(CurLevel, i));
            }

            this.level_data.currentDropLevel(CurLevel);
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(getDropValue(CurLevel, i));
            }
        }

        public float getValue(int type, int level)
        {
            this.level_data.currentLevel(level);
            LevelData leveldata = level_data.getCurrentLevelData();

            switch (type)
            {
                case 0:
                    return leveldata.Power;
                case 1:
                    return leveldata.Damage;
                case 2:
                    return leveldata.Hp;
                case 3:
                    return leveldata.Critical;
                case 4:
                    return leveldata.Defense;
            }

            return 0;
        }

        public float getMobValue(int level, int type)
        {
            this.level_data.currentMobLevel(level);
            MonLevelData leveldata = level_data.getCurrentMobLevelData();

            switch (type)
            {
                case 0:
                    return leveldata.Power;
                case 1:
                    return leveldata.Damage;
                case 2:
                    return leveldata.Hp;
            }

            return 0;
        }

        public int getDropValue(int level, int type)
        {
            this.level_data.currentDropLevel(level);
            MobDropAndScore Dropdata = level_data.getCurrentDropLevelData();

            switch (type)
            {
                case 0:
                    return Dropdata.StageNum;
                case 1:
                    return Dropdata.SubStageNum;
                case 2:
                    return Dropdata.SpawnNum;
                case 3:
                    return Dropdata.Money;
                case 4:
                    return Dropdata.TotalMoney;
                case 5:
                    return Dropdata.Score;
                case 6:
                    return Dropdata.TotalScore;
            }

            return 0;
        }

        public void LevelUp()
        {
            CurLevel++;
        }
    }
}
