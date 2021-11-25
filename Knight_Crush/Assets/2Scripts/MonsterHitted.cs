using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.HeroEditor.FantasyHeroes.MonsterHitted
{
    /// <summary>
    /// Enemy example behaviour.
    /// </summary>
    public class MonsterHitted : MonoBehaviour
    {
        public LoadLevelDataScript Leveldata;
        public GameDataScript GameData;

        public Character Character;
        public SpriteRenderer[] Impact;
        bool Hitted = false, died = false;
        float timecheck = 0;

        public GameObject DamageTxt;
        public GameObject Critical;
        MonsterMovingScript MMoving;
        int hp = 0;
        int Damage = 0;
        public int level;
        PlayerStatus PS;
        bool DropMoney = false;

        public void Start()
        {
            GameData = FindObjectOfType<GameDataScript>();
            DamageTxt = Resources.Load("DamageText") as GameObject;
            Critical = Resources.Load("Critical") as GameObject;
            Leveldata = FindObjectOfType<LoadLevelDataScript>();
            Character = FindObjectOfType<Character>();
            PS = FindObjectOfType<PlayerStatus>();
            if (Character != null)
            {
                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            }

            MMoving = this.gameObject.GetComponent<MonsterMovingScript>();

            hp = (int)Leveldata.getMobValue(level, 2);
            Damage = (int)Leveldata.getMobValue(level, 1);
        }

        private void Update()
        {
            if (hp <= 0)
            {
                if (!DropMoney)
                {
                    DropMoney = true;
                    GameData.ObtainMoney(Leveldata.getDropValue(level, 3));
                }
                died = true;
                ImpactControl(Color.white);
                GetComponent<Monster>().Die();
                MMoving.MonsterDie();
                return;
            }

            if (Hitted)
            {
                ImpactFuc();
                if(count >= 10)
                {
                    ImpactControl(Color.white);
                    count = 0;
                    Hitted = false;
                }
            }
        }

        public void OnDestroy()
        {
            if (Character != null)
            {
                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if (eventName == "Hit" && Vector3.Distance(Character.MeleeWeapon.Edge.position, transform.position) < 1.5f)
            {
                if (died)
                    return;
                MMoving.Detected();
                Vector3 y = this.transform.position + new Vector3(Random.Range(-0.7f, 0.7f), 2 + Random.Range(0, 0.3f), 0);
                Instantiate(DamageTxt, y, Quaternion.identity);

                GetComponent<Monster>().Spring();

                Debug.Log(hp);
                //if(hp <= 0)
                //{
                //    GetComponent<Monster>().Die();
                //    MMoving.MonsterDie();
                //    return;
                //}
                if(Random.Range(0f,100f) < PS.getStatus(1))
                {
                    hp -= PS.getStatus(0);
                    Instantiate(Critical, y, Quaternion.identity);
                }
                hp -= PS.getStatus(0);
                ImpactControl(Color.gray);
                Hitted = true;
            }
        }

        int count = 0;
        public void ImpactFuc()
        {
            Color white = Color.white;
            Color gray = Color.gray;

            timecheck += Time.deltaTime;

            if(timecheck >= 0.1f)
            {
                if(Impact[0].color == white)
                {
                    timecheck = 0;
                    ImpactControl(gray);
                    count++;
                }
                else if(Impact[0].color == gray)
                {
                    timecheck = 0;
                    ImpactControl(white);
                    count++;
                }
            }
        }

        public float TotalHp()
        {
            return Leveldata.getMobValue(level, 2);
        }

        public float curHp()
        {
            if (hp <= 0)
                return 0;
            return (float)hp;
        }

        public void MobDie()
        {
            hp = 0;
        }

        public void ImpactControl(Color color)
        {
            foreach(SpriteRenderer SR in Impact)
            {
                SR.color = color;
            }
        }
    }
}