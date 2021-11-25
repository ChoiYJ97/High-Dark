using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;

public class MoneyScript : MonoBehaviour
{
    public GameDataScript GameData;
    public Text MoneyCount;
    int money = 0;
    float timecheck;

    void Start()
    {
        GameData = FindObjectOfType<GameDataScript>();
        MoneyCount.text = 0.ToString();
        money = 0;
    }

    void Update()
    {
        if (GameData.OwnMoney() >= money)
        {
            this.money = GameData.OwnMoney();
            MoneyCount.text = money.ToString();
            timecheck = 0;
        }
    }

    IEnumerator Count(int target, int current)
    {      
        yield return new WaitForSeconds(0.001f);
        if (target > current)
            this.money += 10;
        else if (target <= current)
            this.money = GameData.OwnMoney();
        MoneyCount.text = money.ToString();
    }

    public void Use(int money)
    {
        this.money -= money;
        GameData.ObtainMoney(-money);
        MoneyCount.text = GameData.OwnMoney().ToString();
    }
}
