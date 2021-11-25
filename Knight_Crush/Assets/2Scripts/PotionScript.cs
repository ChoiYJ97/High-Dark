using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;

public class PotionScript : MonoBehaviour
{
    public GameDataScript GameData;
    public HealthSliderScript PlayerHp;
    public Slider CoolTime;
    public Text Count;
    float timecheck = 0;
    public bool inGame = false;

    void Start()
    {
        GameData = FindObjectOfType<GameDataScript>();
        PlayerHp = FindObjectOfType<HealthSliderScript>();
    }

    void Update()
    {
        if (inGame)
        {
            if (PlayerHp.currentHp() > 0)
            {
                timecheck += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.R) && GameData.getPotionCount() > 0
                    && timecheck >= 1.0f)
                {
                    PlayerHp.FullHealth();
                    GameData.UsePotion();
                    timecheck = 0;
                }
            }
        }
        CoolTimeUpdate();
    }

    public void CoolTimeUpdate()
    {
        CoolTime.value -= Time.deltaTime;
        Count.text = GameData.getPotionCount().ToString();
    }
}
