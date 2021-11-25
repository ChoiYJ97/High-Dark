using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

public class DeadPlaneScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            MonsterHitted MH = other.GetComponent<MonsterHitted>();
            MH.MobDie();
        }
    }
}
