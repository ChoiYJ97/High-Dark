using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.HeroEditor.Common.CharacterScripts;

public class StageSelectSceneNavScript : MonoBehaviour
{
    public NavMeshAgent PlayerNav;
    public Character Player;
    public Transform des;

    void Start()
    {
        des = this.gameObject.transform;
    }

    void Update()
    {
        Moving();
    }

    bool arrived = false;
    public void Moving()
    {
        if (!arrived)
        {
            PlayerNav.destination = des.position;
            Player.SetState(CharacterState.Run);
        }

        if(Vector3.Distance(des.position, gameObject.transform.position) <= 0.5f)
        {
            arrived = true;
            Player.SetState(CharacterState.Idle);
        }
    }

    public void UpdateDestination(Transform destination)
    {
        des = destination; arrived = false;
    }
}
