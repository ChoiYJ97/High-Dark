using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.FantasyMonsters.Scripts;

public class PracticeStudentScript : MonoBehaviour
{
    public Character[] Students;
    public Monster[] Monster;


    void Start()
    {
        Monster = FindObjectsOfType<Monster>();

        if (Monster != null)
        {
            Students[0].Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            Students[1].Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
        }
    }


    float timecheck = 0;

    void Update()
    {
        timecheck += Time.deltaTime;
        if(timecheck >= 2.0f)
        {
            Students[0].Slash();
            Students[1].Slash();
            timecheck = 0;
        }
    }

    public void OnAnimationEvent(string eventName)
    {
        if(eventName == "Hit")
        {
            Monster[0].GetComponent<Monster>().Spring();
            Monster[1].GetComponent<Monster>().Spring();
        }
    }

    public void OnDestroy()
    {
        if (Students[0] != null)
            Students[0].Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
        if (Students[1] != null)
            Students[1].Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
    }
}
