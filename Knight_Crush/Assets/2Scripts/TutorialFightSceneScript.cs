using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

namespace Assets.Origin.Scripts
{
    public class TutorialFightSceneScript : MonoBehaviour
    {
        public Character character;
        public Image Blackout;
        public SpawnMonsterScript Spawner;
        public MovementScript MoveScript;
        public PlayerAttack PAttack;
        CharacterController Controller;
        public HealthSliderScript PlayerHp;
        public GameDataScript GameData;

        public GameObject[] Bubbles;
        public GameObject Panel;
        public MonsterHitted Mob;
        public GameObject[] WinBubbles;
        public GameObject TextofRestart;
        public Transform playerTrans;
        public Transform RevivePos;
        public MonsterMovingScript MobMoveScript;
        public GameObject GoldenSword;
        public GameObject[] SecondSceneBubbles;
        public GameObject[] ThridSceneBubbles;
        public GameObject[] GestureBubbles;
        public GameObject YesOrNo;
        public GameObject SelectJob;

        int SceneNum = 0, SecondSceneNum = 0, ThirdSceneNum = 0;
        float timecheck = 0;
        bool start = false, start2 = false, start3 = false, once = false;
        bool spawn = false, findMob = false;
        bool MobKill = false, clear = false;
        bool over = false, Next = false;
        bool stay = false;
        bool turm = false, turm2 = false;

        void Start()
        {
            GameData = FindObjectOfType<GameDataScript>();
            character = FindObjectOfType<Character>();
            FadeOut();
            MoveScript.StopM();
            PAttack.StopAct();
            foreach (GameObject go in Bubbles)
            {
                go.SetActive(false);
            }
            Panel.SetActive(false);
            foreach (GameObject go in WinBubbles)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in SecondSceneBubbles)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in ThridSceneBubbles)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in GestureBubbles)
            {
                go.SetActive(false);
            }
            PlayerHp = FindObjectOfType<HealthSliderScript>();
            TextofRestart.SetActive(false);
            YesOrNo.SetActive(false);
            SelectJob.SetActive(false); GoldenSword.SetActive(false);
        }

        void Update()
        {
            if (PlayerHp.currentHp() <= 0)
            {
                Panel.SetActive(true);
                TextofRestart.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PlayerHp.FullHealth();
                    Panel.SetActive(false);
                    TextofRestart.SetActive(false);                   
                    character.SetState(CharacterState.Idle);
                    MobMoveScript.PlayerRevive();
                }
            }
            else
            {
                if (!Next)
                {
                    FirstScenePlay();   
                }
                else if (Next && !start3)
                {
                    SecondScenePlay();
                }     
                else if (start3)
                {
                    SelectJobScenePlay();
                }
            }
        }

        private void LateUpdate()
        {
            if (spawn && !findMob)
            {
                Mob = FindObjectOfType<MonsterHitted>();
                MobMoveScript = FindObjectOfType<MonsterMovingScript>();
                findMob = true;
            }
        }

        public void FadeOut()//화면 밝아지기
        {
            StartCoroutine(FadeOutCoroutine());
        }
        public void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
        }
        IEnumerator FadeOutCoroutine()
        {
            float fadeCount = 1;
            while (fadeCount > 0.0f)
            {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }
        IEnumerator FadeInCoroutine()//화면 어둡게하기
        {
            float fadeCount = 0;
            while (fadeCount < 1.0f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }
        IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            MoveScript.StopM();
            PAttack.StopAct();
            Panel.SetActive(true);
            WinBubbles[0].SetActive(true);
            character.SetState(CharacterState.Idle);
        }
        IEnumerator SceneDelay()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(MobMoveScript.gameObject);
            Next = true;
            once = true;
            start = false;
            timecheck = 0;
            playerTrans.position = RevivePos.position;
            if (once)
            {
                once = false;
                FadeOut();
            }
            Panel.SetActive(false);
            MoveScript.StopM();
            PAttack.StopAct();
        }

        public void Move()
        {
            var direction = Vector3.zero;
            direction.x = 0.8f;
            Vector3 speed = new Vector3(3f * direction.x, 0, 6 * direction.z);

            character.transform.localScale = new Vector3(Mathf.Sign(speed.x) * 0.7f, 0.7f, 1);

            if (direction != Vector3.zero)
                character.SetState(CharacterState.Run);
            else if (character.GetState() < CharacterState.DeathB)
                character.SetState(CharacterState.Idle);

            speed.y -= 1000 * Time.deltaTime;
            Controller.Move(speed * Time.deltaTime);
        }

        public void BubbleControl(int i)
        {
            switch (i)
            {
                case 0:
                    Bubbles[i].SetActive(true);
                    break;
                case 1:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    break;
                case 2:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    Panel.SetActive(true);
                    break;
                case 3:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    break;
                case 4:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    Panel.SetActive(false);
                    break;
                case 5:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    break;
                default:
                    foreach (GameObject go in Bubbles)
                    {
                        go.SetActive(false);
                    }
                    if (SceneNum >= 6)
                        once = true;
                    break;
            }
        }
        public void SecondBubbleControl(int i)
        {
            switch (i)
            {
                case 0:
                    SecondSceneBubbles[i].SetActive(true);
                    break;
                case 1:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 2:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 3:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;

                    //텀

                case 4:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 5:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 6:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 7:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 8:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;
                case 9:
                    SecondSceneBubbles[i].SetActive(true);
                    SecondSceneBubbles[i - 1].SetActive(false);
                    break;


                default:
                    foreach (GameObject go in SecondSceneBubbles)
                    {
                        go.SetActive(false);
                    }
                    break;
            }
        }
        public void ThirdBubbleControl(int i)
        {
            switch (i)
            {
                case 0:
                    ThridSceneBubbles[i].SetActive(true);
                    break;
                case 1:
                    ThridSceneBubbles[i].SetActive(true);
                    ThridSceneBubbles[i - 1].SetActive(false);
                    break;
                case 2:
                    ThridSceneBubbles[i].SetActive(true);
                    ThridSceneBubbles[i - 1].SetActive(false);
                    break;
                case 3:
                    ThridSceneBubbles[i].SetActive(true);
                    ThridSceneBubbles[i - 1].SetActive(false);
                    break;
                default:
                    foreach (GameObject go in ThridSceneBubbles)
                    {
                        go.SetActive(false);
                    }
                    break;
            }
        }

        public void FirstScenePlay()
        {
            if (!once)
            {
                timecheck += Time.deltaTime;

                if (timecheck >= 0.1f && !start)
                {
                    Controller = MoveScript.CharController();
                    if (timecheck >= 0.5f)
                        Move();
                }

                if (timecheck >= 1.5f && !start)
                {
                    start = true;
                    character.SetState(CharacterState.Idle);
                    BubbleControl(SceneNum);
                    timecheck = 0;
                }

                if (Input.GetKeyDown(KeyCode.Return) && timecheck >= 0.2f && start)
                {
                    timecheck = 0;
                    SceneNum++;
                    BubbleControl(SceneNum);
                    Debug.Log("SceneNum : " + SceneNum.ToString());
                }
            }
            else
            {
                if (!spawn)
                {
                    Spawner.Spawn(Spawner.MobData(1));
                    spawn = true;
                    MoveScript.StartM();
                    PAttack.StartAct();
                }
            }

            if (findMob && !MobKill && SceneNum == 6)
            {
                if (Mob.curHp() <= 0)
                {
                    MobKill = true;
                    findMob = false;
                    StartCoroutine(Delay(1.0f));
                }
            }

            if (MobKill && !clear && SceneNum == 6)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    WinBubbles[1].SetActive(true);
                    WinBubbles[0].SetActive(false);
                    clear = true;
                    SceneNum++;
                }
            }

            else if (clear && Input.GetKeyDown(KeyCode.Return) && !over && SceneNum == 7)
            {
                over = true;
                WinBubbles[1].SetActive(false);
                WinBubbles[0].SetActive(false);
                FadeIn();
                SceneNum++;
            }

            else if (over)
            {
                Move();
                if (!stay)
                {
                    stay = true;
                    StartCoroutine(SceneDelay());
                }
            }

        }
        public void SecondScenePlay()
        {
            if (!once)
            {
                if(!turm)
                    timecheck += Time.deltaTime;               
                if (timecheck >= 0.1f && !start)
                {
                    GoldenSword.SetActive(true);
                    Controller = MoveScript.CharController();
                    if (timecheck >= 0.5f)
                        Move();
                }

                if (timecheck >= 1.5f && !start)
                {
                    start = true;
                    character.SetState(CharacterState.Idle);
                    SecondBubbleControl(SecondSceneNum);
                    timecheck = 0;
                }

                if (Input.GetKeyDown(KeyCode.Return) && timecheck >= 0.2f && start && !turm)
                {
                    timecheck = 0;
                    SecondSceneNum++;
                    SecondBubbleControl(SecondSceneNum);
                    Debug.Log("SecondSceneNum : " + SecondSceneNum.ToString());
                    if (SecondSceneNum == 3)
                    {
                        turm = true;
                        timecheck = 0;
                    }
                }

                if(turm && !turm2 && Input.GetKeyDown(KeyCode.Return))
                {
                    turm2 = true;
                    timecheck = 0;
                }
                if (turm2)
                    timecheck += Time.deltaTime;

                if (turm && turm2)
                {
                    Controller = MoveScript.CharController();
                    Move();
                    if(timecheck >= 2.0f)
                    {
                        once = true;
                        timecheck = 0;
                    }
                }
            }

            else if (once)
            {
                timecheck += Time.deltaTime;
                if (turm && turm2)
                {
                    SecondSceneNum++;
                    SecondBubbleControl(SecondSceneNum);
                    turm = false;
                    turm2 = false;
                    over = false;
                    timecheck = 0;
                    character.SetState(CharacterState.Idle);
                }

                if (timecheck >= 0.5f && timecheck < 1.0f && !start2)
                    GestureBubbles[0].SetActive(true);

                if (timecheck >= 1f && !start2)
                {
                    GestureBubbles[1].SetActive(true);
                    GestureBubbles[0].SetActive(false);
                }

                if (timecheck >= 1.5f && Input.GetKeyDown(KeyCode.Return) && !start2)
                {
                    start2 = true;
                    GestureBubbles[1].SetActive(false);
                    SecondSceneNum++;
                    SecondBubbleControl(SecondSceneNum);
                    timecheck = 0;
                }

                if(start2 && Input.GetKeyDown(KeyCode.Return) && timecheck >= 0.2f && !over)
                {
                    if (SecondSceneNum >= 9)
                    {
                        over = true;
                    }
                    SecondSceneNum++;
                    SecondBubbleControl(SecondSceneNum);                    
                }

                if(over && SecondSceneNum == 10)
                {
                    Panel.SetActive(true);
                    YesOrNo.SetActive(true);
                    SecondSceneNum++;
                }
            }
        }
        public void SelectJobScenePlay()
        {
            timecheck += Time.deltaTime;
            if (!once)
            {
                once = true;
                ThirdSceneNum = 0;
                ThirdBubbleControl(ThirdSceneNum);
                timecheck = 0;
            }

            if (once)
            {
                if (Input.GetKeyDown(KeyCode.Return) && timecheck >= 0.2f && ThirdSceneNum < 1)
                {
                    timecheck = 0;
                    ThirdSceneNum++;
                    ThirdBubbleControl(ThirdSceneNum);
                }

                if (Input.GetKeyDown(KeyCode.Return)  && ThirdSceneNum == 1 && timecheck >= 0.4f)
                {
                    OpenJobWindow();
                }

                if(Input.GetKeyDown(KeyCode.Return) && ThirdSceneNum == 2)
                {
                    ThirdSceneNum++;
                    ThirdBubbleControl(ThirdSceneNum);
                    timecheck = 0;
                }

                if (Input.GetKeyDown(KeyCode.Return) && ThirdSceneNum == 3 && timecheck > 0.3f)
                {
                    ThirdSceneNum++;
                    ThirdBubbleControl(-1);
                    FadeIn();
                    timecheck = 0;
                    GameData.TutorialClear();
                }

                if(ThirdSceneNum == 4 && timecheck >= 1.2f)
                {
                    //SceneManagerScript._instance.GoStageSelectScene();
                    SceneManagerScript._instance.GoStageSelectScene();
                }
            }
        }


        public void ClickYes()
        {
            Panel.SetActive(false);
            YesOrNo.SetActive(false);
            start3 = true;
            once = false;
            timecheck = 0;
        }

        public void OpenJobWindow()
        {
            ThirdBubbleControl(-1);
            Panel.SetActive(true);
            SelectJob.SetActive(true);
        }

        public void CloseJobWindow()
        {
            Panel.SetActive(false);
            SelectJob.SetActive(false);
            ThirdSceneNum++;
            ThirdBubbleControl(ThirdSceneNum);
        }
    }  
}