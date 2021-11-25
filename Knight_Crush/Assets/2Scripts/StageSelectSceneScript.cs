using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneScript : MonoBehaviour
{
    public GameDataScript GameData;
    public Sprite RedButton, BrownButton;
    public SpriteRenderer[] Buttons;
    public SpriteRenderer[] stage1Mob;
    public SpriteRenderer[] stage2Mob;
    public Image Blackout;
    public GameObject[] StageButtons;
    public GameObject InfinityButtons;
    public GameObject ShopButtons;
    public bool isTutorialPlace = true;
    public bool TutorialClear = false;
    public int currentStage = 0;
    bool once = false;

    void Start()
    {
        GameData = FindObjectOfType<GameDataScript>();
        FadeOut();
        IsTutorialClear();
    }

    void Update()
    {
        if (!TutorialClear)
        {
            if (!once)
            {
                Buttons[0].sprite = RedButton;
                foreach (SpriteRenderer SR in stage1Mob)
                {
                    SR.color = Color.black;
                }
                Buttons[1].sprite = RedButton;
                foreach (SpriteRenderer SR in stage2Mob)
                {
                    SR.color = Color.black;
                }
                StageButtons[0].SetActive(false);
                StageButtons[1].SetActive(false);
                InfinityButtons.GetComponent<Button>().interactable = false;
                ShopButtons.GetComponent<Button>().interactable = false;
                isTutorialPlace = true;
                once = true;
            }
            
        }

        else if (TutorialClear)
        {
            if (!once)
            {
                if (GameData.GetCurrentStage() >= 0)
                {
                    Buttons[0].sprite = BrownButton;
                    foreach (SpriteRenderer SR in stage1Mob)
                    {
                        SR.color = Color.white;
                    }
                    StageButtons[0].SetActive(true);
                    ShopButtons.GetComponent<Button>().interactable = true;
                }

                if(GameData.GetCurrentStage() >= 1)
                {
                    Buttons[1].sprite = BrownButton;
                    foreach (SpriteRenderer SR in stage2Mob)
                    {
                        SR.color = Color.white;
                    }
                    InfinityButtons.GetComponent<Button>().interactable = true;
                    StageButtons[1].SetActive(true);
                }
                once = true;
            }
        }
    }

    public void SetStageNum(int num) //버튼에 설정
    {
        currentStage = num;
    }

    public void IsTutorialClear()
    {
        if(GameData.getTutorialClear())
            TutorialClear = GameData.getTutorialClear();
    }

    public void GoAdventure(bool infinity) //버튼에 설정
    {
        if (!TutorialClear || isTutorialPlace)
            GoTutorial();
        if(TutorialClear)
            StartCoroutine(GoStageDelay(infinity, currentStage));
    }
    IEnumerator GoStageDelay(bool Infinity, int StageNum)
    {
        FadeIn();
        GameData.setInfinity(Infinity);
        if (Infinity)
            GameData.SetStageNum(0);
        else if (!Infinity)
        {
            GameData.SetStageNum(StageNum);
        }
        yield return new WaitForSeconds(1.0f);
        SceneManagerScript._instance.GoStageScene();
    }
    public void GoTutorial()
    {
        StartCoroutine(GoTutorialDelay());
    }
    IEnumerator GoTutorialDelay()
    {
        FadeIn();
        yield return new WaitForSeconds(1.0f);
        SceneManagerScript._instance.GoStoryScene();
    }
    public void isTutorial()
    {
        isTutorialPlace = true;
    }
    public void isNotTutorial()
    {
        isTutorialPlace = false;
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
