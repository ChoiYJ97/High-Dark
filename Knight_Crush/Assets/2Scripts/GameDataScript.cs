using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;
using HeroEditor.Common;

public class GameDataScript : MonoBehaviour
{
    public SpriteGroupEntry CurrentWeapon;
    public int StageNumber = 0;         // 현재 스테이지
    public int Money = 0;               // 재화
    public int PotionCount = 10;        // 물약
    public int Score = 0;               // 점수 (사용할지 말지 고민)
    public int PlayerLevel = 0;         // 현재 플레이어 레벨    
    public int clearedStage = -1;       //클리어된 스테이지
    public bool isInfinityMode = false; // 무한모드 입장
    public bool TutorialCleared = false;// 튜토리얼 클리어 여부

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public SpriteGroupEntry getWeapon()
    {
        return CurrentWeapon;
    }
    public void setWeapon(SpriteGroupEntry item)
    {
        CurrentWeapon = item;
    }
    public void SetStageNum(int num)
    {
        StageNumber = num;
    }
    public int GetStageNum()
    {
        return StageNumber;
    }

    public void SetPotionCount(int count)
    {
        PotionCount = count;
    }
    public void ObtainPotion()
    {
        PotionCount++;
    }
    public void UsePotion()
    {
        PotionCount--;
    }
    public int getPotionCount()
    {
        return PotionCount; 
    }

    public void ObtainMoney(int money)
    {
        Money += money;
    }
    public int OwnMoney()
    {
        return Money;
    }

    public bool getTutorialClear() //튜토리얼 클리어
    {
        return TutorialCleared;
    }

    public void TutorialClear()
    {
        TutorialCleared = true;
        clearedStage = 0;
    }

    public int GetCurrentStage()
    {
        return clearedStage;
    }
    public void SetCurrentStage(int num)
    {
        clearedStage = num;
    }

    public int getPlayerLevel()
    {
        return PlayerLevel;
    }
    public void setPlayerLevel(int level)
    {
        PlayerLevel = level;
    }

    public void setInfinity(bool check)
    {
        isInfinityMode = check;
    }
    public bool getInfinity()
    {
        return isInfinityMode;
    }
}
