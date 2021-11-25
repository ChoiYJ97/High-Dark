using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Origin.Scripts
{
    public class LevelData : MonoBehaviour
    {
        public float Power;
        public float Damage;
        public float Hp;
        public float Critical;
        public float NotCritical;
        public float Defense;

        public LevelData()
        {
            Power = 0;
            Damage = 0;
            Hp = 0;
            Critical = 0;
            NotCritical = 100;
            Defense = 0;
        }
    }
    public class MonLevelData : MonoBehaviour
    {
        public float Power;
        public float Damage;
        public float Hp;

        public MonLevelData()
        {
            Power = 0;
            Damage = 0;
            Hp = 0;
        }
    }
    public class MobDropAndScore : MonoBehaviour
    {
        public int StageNum;
        public int SubStageNum;
        public int SpawnNum;
        public int Money;
        public int TotalMoney;
        public int Score;
        public int TotalScore;

        public MobDropAndScore()
        {
            StageNum = 0;
            SubStageNum = 0;
            SpawnNum = 0;
            Money = 0;
            TotalMoney = 0;
            Score = 0;
            TotalScore = 0;
        }
    }

    public class LevelDesignData : MonoBehaviour
    {
        private List<LevelData> level_datas = null;
        private List<MonLevelData> M_level_datas = null;
        private List<MobDropAndScore> M_Drop_datas = null;
        int level = 0;
        int M_level = 0;
        int D_level = 0;

        public void initialize()
        {
            this.level_datas = new List<LevelData>();
            this.M_level_datas = new List<MonLevelData>();
            this.M_Drop_datas = new List<MobDropAndScore>();
        }

        public void LoadLevelData(TextAsset level_data_text)
        {
            string level_texts = level_data_text.text;
            string[] lines = level_texts.Split('\n');

            foreach(var line in lines)
            {
                if(line == "")
                {
                    continue;
                }

                string[] words = line.Split();
                int n = 0;

                LevelData level_data = gameObject.AddComponent<LevelData>();

                foreach(var word in words)
                {
                    if (word.StartsWith("#")) { break; }
                    if(word == "") { continue; }
                    switch (n)
                    {
                        case 0: level_data.Power = float.Parse(word); break;
                        case 1: level_data.Damage = float.Parse(word); break;
                        case 2: level_data.Hp = float.Parse(word); break;
                        case 3: level_data.Critical = float.Parse(word);
                                level_data.NotCritical = (100 - level_data.Critical); break;
                        case 4: level_data.Defense = float.Parse(word); break;
                    }
                    n++;
                }

                if (n >= 5)
                {
                    this.level_datas.Add(level_data);
                }
                else
                {
                    if (n == 0) { }
                    else
                    {
                        Debug.LogError("[LevelData] Out of parameter.\n");
                    }
                }
            }

            if (this.level_datas.Count == 0)
            {
                Debug.LogError("[LevelData] Has no data.\n");
                this.level_datas.Add(new LevelData());
            }
        }
        public void Load_Mon_LevelData(TextAsset level_data_text)
        {
            string level_texts = level_data_text.text;
            string[] lines = level_texts.Split('\n');

            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }

                string[] words = line.Split();
                int n = 0;

                MonLevelData level_data = gameObject.AddComponent<MonLevelData>();

                foreach (var word in words)
                {
                    if (word.StartsWith("#")) { break; }
                    if (word == "") { continue; }
                    switch (n)
                    {
                        case 0: level_data.Power = float.Parse(word); break;
                        case 1: level_data.Damage = float.Parse(word); break;
                        case 2: level_data.Hp = float.Parse(word); break;
                    }
                    n++;
                }

                if (n >= 3)
                {
                    this.M_level_datas.Add(level_data);
                }
                else
                {
                    if (n == 0) { }
                    else
                    {
                        Debug.LogError("[LevelData] Out of parameter.\n");
                    }
                }
            }

            if (this.M_level_datas.Count == 0)
            {
                Debug.LogError("[LevelData] Has no data.\n");
                this.M_level_datas.Add(new MonLevelData());
            }
        }
        public void Load_Mob_DropData(TextAsset Drop_data_text)
        {
            string Drop_Texts = Drop_data_text.text;
            string[] lines = Drop_Texts.Split('\n');

            foreach (var line in lines)
            {
                if(line == "")
                {
                    continue;
                }

                string[] words = line.Split();

                int n = 0;

                MobDropAndScore Drop_data = gameObject.AddComponent<MobDropAndScore>();
                foreach(var word in words)
                {
                    if (word.StartsWith("#")) { break; }
                    if (word == "") { continue; }
                    switch (n)
                    {
                        case 0: int value = int.Parse(word);
                                Drop_data.StageNum = value/10;
                                Drop_data.SubStageNum = value % 10; break;
                        case 1: Drop_data.SpawnNum = int.Parse(word); break;
                        case 2: Drop_data.Money = int.Parse(word);break;
                        case 3: Drop_data.TotalMoney = int.Parse(word);break;
                        case 4: Drop_data.Score = int.Parse(word);break;
                        case 5: Drop_data.TotalScore = int.Parse(word);break;
                    }
                    n++;
                }

                if (n >= 6)
                {
                    this.M_Drop_datas.Add(Drop_data);
                }
                else
                {
                    if (n == 0) { }
                    else
                    {
                        Debug.LogError("[DropData] Out of Parameter.\n");
                    }
                }
            }

            if(this.M_Drop_datas.Count == 0)
            {
                Debug.LogError("[LevelData] Has no data.\n");
                this.M_Drop_datas.Add(new MobDropAndScore());
            }
        }

        public void currentLevel(int level)
        {
            this.level = level;
        }
        public void currentMobLevel(int level)
        {
            this.M_level = level;
        }
        public void currentDropLevel(int level)
        {
            this.D_level = level;
        }

        public LevelData getCurrentLevelData()
        {
            return (this.level_datas[this.level]);
        }
        public MonLevelData getCurrentMobLevelData()
        {
            return (this.M_level_datas[this.M_level]);
        }
        public MobDropAndScore getCurrentDropLevelData()
        {
            return (this.M_Drop_datas[this.D_level]);
        }
    }
}

