using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

namespace Assets.Origin.Scripts
{
    public class StageManagerScript : MonoBehaviour
    {
        public GameDataScript GameData;
        public Character character;
        public Transform PlayerTrans;
        public SpawnMonsterScript Spawn;
        public MonsterHitted[] Mobs;
        public MonsterHitted EliteMobs;
        public MonsterMovingScript[] MobMove;
        public HealthSliderScript PlayerHp;
        public Transform RevivePos;
        public GameObject StageTxtObj, ImStageTxtObj;
        public Text StageTxt, ImStageText, TimeText;
        public LoadLevelDataScript LevelData;
        public GameObject Potal;
        public GameObject Clear;
        public PlayerAttack PAttack;
        public MovementScript MoveScript;
        public Image Blackout;
        public Slider ProgressGuage;
        public GameObject YouDied;

        float elapsedTime = 0;

        bool GameStart = false;
        bool EliteMobDied = false;
        bool AllMobsDied = false;
        bool IMStart = false;
        bool once = false;

        float timecheck, ReviveDelay = 0;
        int MonsterCountForIM = 0;
        int IMStage = 0, IMSubStage = 0;


        public bool InfinityMode = false;
        public int StageNum = 1;
        public int SubStageNum = 1;
        public int Moblevel = 0;
        int MobCount = 0;
        int ReviveCount = 3;
        bool NormalStart = false, NAllMobDied = false;
        bool NormalNextStage = false, Normalonce = false;
        bool isDelay = false, StageClear = false;

        bool timePause = false;

        void Start()
        {
            GameData = FindObjectOfType<GameDataScript>();
            character = FindObjectOfType<Character>();
            PlayerTrans = character.gameObject.GetComponent<Transform>();
            PlayerHp = FindObjectOfType<HealthSliderScript>();
            LevelData = FindObjectOfType<LoadLevelDataScript>();
            PAttack = FindObjectOfType<PlayerAttack>();
            MoveScript = FindObjectOfType<MovementScript>();
            Potal.SetActive(false);
            StageTxtObj.SetActive(false);
            ImStageTxtObj.SetActive(false);
            Clear.SetActive(false);
            YouDied.SetActive(false);
            FadeOut();
            StageNum = GameData.GetStageNum();
            SubStageNum = 1; ReviveCount = 3;
            ProgressGuage.value = 0.33f * SubStageNum;
            switch (StageNum)
            {
                case 1:
                    Moblevel = 0;
                    break;
                case 2:
                    Moblevel = 13;
                    break;
                default:
                    Moblevel = 0;
                    break;
            }
        }

        public void GetStageNum(bool infinityMode, int stageNum)
        {
            this.InfinityMode = infinityMode;
            if (!InfinityMode)
            {
                this.StageNum = stageNum;
                StageTxt.text = StageNum.ToString();
            }
        }
        
        void Update()
        {
            if (!GameStart)
            {
                StartCoroutine(GameDelay());
            }
            if (GameStart)
            {
                if(!timePause) UpdateTimeText();

                if (InfinityMode && IMStage != 26)
                {
                    InfinityModeController();
                    ImStageTxtObj.SetActive(true);

                    if (PlayerHp.currentHp() <= 0)
                    {
                        ReviveDelay += Time.deltaTime;
//                        if(ReviveCount <= 0)
                        {
                            timePause = true;
                            YouDied.SetActive(true);
                        }
                        //if (Input.GetKeyDown(KeyCode.R) && ReviveDelay >= 1.5f && ReviveCount > 0)
                        //{
                        //    ReviveCount--;
                        //    PAttack.StopAct();
                        //    MoveScript.StopM();
                        //    //PlayerTrans.Translate(RevivePos.position);
                        //    PlayerTrans.position = RevivePos.position;
                        //    PlayerHp.FullHealth();
                        //    character.SetState(CharacterState.Idle);

                        //    foreach (MonsterMovingScript MM in MobMove)
                        //    {
                        //        MM.PlayerRevive();
                        //    }

                        //    PAttack.StartAct();
                        //    MoveScript.StartM();

                        //    ReviveDelay = 0;
                        //}
                    }

                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        foreach (MonsterHitted MH in Mobs)
                        {
                            MH.MobDie();
                        }
                        if(EliteMobs != null)
                            EliteMobs.MobDie();
                    }
                }
                if(InfinityMode && IMStage == 26)
                {
                    GameStart = false;
                    StageClear = true;
                }

                if (!InfinityMode)
                {
                    StageTxtObj.SetActive(true);
                    NormalModeController();

                    if (PlayerHp.currentHp() <= 0)
                    {
 //                       if (ReviveCount <= 0)
                        {
                            timePause = true;
                            YouDied.SetActive(true);
                        }
//                        if (Input.GetKeyDown(KeyCode.R) && ReviveCount > 0)
//                            PlayerHp.FullHealth();
                    }

                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        foreach (MonsterHitted MH in Mobs)
                        {
                            MH.MobDie();
                        }
                    }
                }
            }
            
            if(!GameStart && StageClear)
            {
                Potal.SetActive(false);
                timePause = true;
                PAttack.StopAct();
                MoveScript.StopM();
                character.SetState(CharacterState.Idle);
                Clear.SetActive(true);
                GameData.SetCurrentStage(StageNum);
            }
        }

        int hour = 0, min = 0;
        public void UpdateTimeText()
        {
            elapsedTime += Time.deltaTime;
            min = (int)elapsedTime;
            if(elapsedTime >= 60f)
            {
                min -= 60;
                elapsedTime -= 60;
                hour += 1;
            }
            TimeText.text = hour + ":" + min;
        }

        public IEnumerator GameDelay()
        {
            yield return new WaitForSeconds(0.1f);
            InfinityMode = GameData.getInfinity();
            if (!InfinityMode)
            {
                StageNum = GameData.GetStageNum();
            }
            yield return new WaitForSeconds(1.0f);
            GameStart = true;
        }

        public void InfinityModeController()
        {
            if (!IMStart && MonsterCountForIM == 0)
            {
                if (IMSubStage == 0 || IMSubStage >= 4)
                {
                    IMStage++;
                    Debug.Log(IMStage + "stage");
                    IMSubStage = 1;
                }

                if (IMStage == 26)
                    return;

                switch (IMSubStage)
                {
                    case 1:
                        IMSpawnMobs(3, IMStage);
                        EliteMobDied = true;
                        break;
                    case 2:
                        IMSpawnMobs(4, IMStage);
                        EliteMobDied = true;
                        break;
                    case 3:
                        IMSpawnMobs(4, IMStage);
                        IMSpawnEliteMob(IMStage);
                        EliteMobDied = false;
                        break;
                    default:
                        break;
                }
                IMStart = true;
                ImStageText.text = IMStage.ToString() + "-" + IMSubStage.ToString();
            }

            else if(IMStart || MonsterCountForIM >= 1)
            {
                int count = 0;
                if (!AllMobsDied)
                {
                    for (int i = 0; i < MonsterCountForIM; i++)
                    {
                        if (Mobs[i].curHp() <= 0)
                        {
                            count++;
                        }

                        if (count >= MonsterCountForIM)
                        {
                            AllMobsDied = true;
                        }
                    }
                }

                if (EliteMobs == null ||EliteMobs.curHp() <= 0)
                    EliteMobDied = true;
            }

            if (AllMobsDied && EliteMobDied && !once)
            {
                once = true;
                StartCoroutine(IMStartDelay());
            }
        }
        public void IMSpawnMobs(int SpawnNum, int StageNum)
        {
            AllMobsDied = false;
            for (int i = 0; i < SpawnNum; i++)
            {
                Spawn.Spawn(Spawn.MobData(StageNum - 1));
                MonsterCountForIM++;
            }
            Mobs = FindObjectsOfType<MonsterHitted>();
            MobMove = FindObjectsOfType<MonsterMovingScript>();
        }
        public void IMSpawnEliteMob(int StageNum)
        {
            Spawn.Spawn(Spawn.MobData(StageNum));
            EliteMobs = FindObjectOfType<MonsterHitted>();
        }
        public IEnumerator IMStartDelay()
        {
            yield return new WaitForSeconds(1.0f);
            foreach (MonsterHitted Mob in Mobs)
            {
                Destroy(Mob.gameObject);
            }
            if (EliteMobs != null)
                Destroy(EliteMobs.gameObject);
            MonsterCountForIM = 0;
            yield return new WaitForSeconds(2.0f);
            IMStart = false;
            AllMobsDied = false;
            once = false;
            IMSubStage++;
        }
        public IEnumerator NormalStartDelay()
        {
            yield return new WaitForSeconds(1.0f);
            foreach(MonsterHitted Mob in Mobs)
            {
                Destroy(Mob.gameObject);
            }
            PlayerTrans.position = RevivePos.position;
            Potal.SetActive(false);
            FadeOut();
            yield return new WaitForSeconds(1.0f);
            NormalStart = false;
            isDelay = false;
            MobCount = 0;
            Debug.Log(SubStageNum);
            if (SubStageNum <= 2)
                SubStageNum++;

            PAttack.StartAct();
            MoveScript.StartM();            
        }
        public void NormalModeController()
        {
            if (isDelay && Normalonce)
            {
                Normalonce = false;
                StartCoroutine(NormalStartDelay());
            }

            if (!NormalStart && !isDelay)
            {
                NormalSpawnMobs();               
                timePause = false;
                foreach (MonsterHitted M in Mobs)
                {
                    MobCount++;
                    Debug.Log("Mobs : " + MobCount);
                }
                NAllMobDied = false;
                NormalStart = true;
            }

            else if (NormalStart && !isDelay)
            {
                int count = 0;
                if (!NAllMobDied && !NormalNextStage)
                {
                    for(int i = 0; i < MobCount; i++)
                    {
                        if(Mobs[i].curHp() <= 0)
                        {
                            count++;
                        }
                        if (count >= MobCount)
                        { NAllMobDied = true;}
                    }
                }

                if(NAllMobDied)
                {                    
                    Potal.SetActive(true);
                    //timePause = true;
                    NormalNextStage = true;
                    if (SubStageNum == 3)
                    {
                        Potal.SetActive(false);
                        StageClear = true;
                        GameStart = false;
                    }
                }

                if (NormalNextStage && !Normalonce && SubStageNum < 3)
                {
                    Vector3 playerPos = new Vector3(PlayerTrans.position.x, 0, 0);
                    Vector3 PotalPos = new Vector3(Potal.GetComponent<Transform>().position.x, 0, 0);
                    float Dis = Vector3.Distance(playerPos, PotalPos);
                    if(Dis <= 1.0f)
                    {
                        isDelay = true;                       
                        NormalStart = false;
                        Normalonce = true;
                        NormalNextStage = false;
                        PAttack.StopAct();
                        MoveScript.StopM();
                        character.SetState(CharacterState.Idle);
                        FadeIn();
                    }
                }
            }
        }
        public void NormalSpawnMobs()
        {
            while (true)
            {
                if (LevelData.getDropValue(Moblevel, 1) != SubStageNum)
                    break;
                if (LevelData.getDropValue(Moblevel, 0) == StageNum &&
                LevelData.getDropValue(Moblevel, 1) == SubStageNum)
                {
                    for (int i = 0; i < LevelData.getDropValue(Moblevel, 2); i++)
                    {
                        Spawn.Spawn(Spawn.MobData(Moblevel));
                    }
                    Moblevel++;
                }
            }
            ProgressGuage.value = (0.33f * (float)SubStageNum);
            StageTxt.text = StageNum + "-" + SubStageNum + " Stage";
            Mobs = FindObjectsOfType<MonsterHitted>();
            MobMove = FindObjectsOfType<MonsterMovingScript>();
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
                fadeCount += 0.03f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }
    }
}