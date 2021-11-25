using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;

namespace Assets.Origin.Scripts
{
    public class MonsterMovingScript : MonoBehaviour
    {
        public Character Character;
        public Transform PlayerTrans;
        public CharacterController Controller;
        Vector3 speed;

        public Monster Monster;
        public Transform MonsterTrans;
        public GameObject DetectedIcon;

        public Animator ani;

        public Assets.FantasyMonsters.Scripts.LayerManager layer;

        bool detected, PlayerDied, MobDied;
        float timecheck;

        void Start()
        {
            layer = this.gameObject.GetComponent<Assets.FantasyMonsters.Scripts.LayerManager>();
            PlayerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
            Character = FindObjectOfType<Character>();

            Monster = this.gameObject.GetComponent<Monster>();
            MonsterTrans = Monster.transform;
            DetectedIcon = MonsterTrans.Find("DetectedIcon").gameObject;
            ani = Monster.GetComponent<Animator>();

            if (Controller == null)
            {
                Controller = this.Monster.gameObject.AddComponent<CharacterController>();
                Controller.center = new Vector3(0, 1.27f);
                Controller.height = 1.0f;
                Controller.radius = 0.75f;
                Controller.minMoveDistance = 0;
            }
            Monster.Animator.SetBool("Action", true);

            timecheck = 0;

            detected = false;
            PlayerDied = false;
            MobDied = false;

            DetectedIcon.SetActive(false);
            layer.SetSortingGroupOrder(0);
        }

        Vector3 direction;
        void Update()
        {
            if (MobDied)
            {
                DetectedIcon.SetActive(false);
                return;
            }
            if (detected && !PlayerDied)
            {
                timecheck += Time.deltaTime;
                if (timecheck >= 1.0f)
                {
                    DetectedIcon.SetActive(false);
                    timecheck = 0;
                }

                ChaseMove();

                if (Vector2.Distance(PlayerTrans.position, MonsterTrans.position) <= 1.5f)
                {
                    ani.SetTrigger("Attack");
                }
                return;
            }

            timecheck += Time.deltaTime;
            float RandomDelay = Random.Range(0.6f, 1.5f);

            if (direction == Vector3.zero)
                RandomDelay = 2.1f;

            if (timecheck > RandomDelay)
            {
                direction = Vector3.zero;

                RandomDir();

                timecheck = 0;
            }

            Monster.Animator.SetBool("Action", false);
            Move(direction);

            if (Vector3.Distance(PlayerTrans.position, MonsterTrans.position) < 5.0f && !PlayerDied)
            {
                detected = true;
                timecheck = 0;
                DetectedIcon.SetActive(true);
            }
        }

        public void RandomDir()
        {
            int range = 20;
            int randomValue = Random.Range(0, range);

            if (randomValue < 2 && randomValue >= 0) { direction.x = 0; direction.z = 0; }

            if (randomValue < 4 && randomValue >= 2) { direction.x = -0.4f; direction.z = 0.4f; }
            if (randomValue < 6 && randomValue >= 4) { direction.x = -0.4f; direction.z = -0.4f; }
            if (randomValue < 8 && randomValue >= 6) { direction.x = 0.4f; direction.z = -0.4f; }
            if (randomValue < 10 && randomValue >= 8) { direction.x = 0.4f; direction.z = 0.4f; }

            if (randomValue < 12 && randomValue >= 10) { direction.x = -0.4f; direction.z = 0; }
            if (randomValue < 14 && randomValue >= 12) { direction.x = 0.4f; direction.z = 0; }
            if (randomValue < 16 && randomValue >= 14) { direction.x = 0; direction.z = -0.4f; }
            if (randomValue < 18 && randomValue >= 16) { direction.x = 0; direction.z = 0.4f; }

            if (randomValue <= 20 && randomValue >= 18) { direction.x = 0; direction.z = 0; }
        }

        public void Move(Vector3 direction)
        {
            speed = new Vector3(5 * direction.x, 0, 10 * direction.z);

            if (direction != Vector3.zero)
            {
                Turn(speed.x);
            }

            if (direction != Vector3.zero)
            { Monster.SetState(MonsterState.Walk); }
            else if (Monster.GetState() < MonsterState.Death)
            { Monster.SetState(MonsterState.Idle); }

            speed.y -= 1000 * Time.deltaTime;

            Controller.Move(speed * Time.deltaTime);
        }

        public void ChaseMove()
        {
            Vector3 vectorValue;
            vectorValue = PlayerTrans.position - MonsterTrans.position;
            speed = new Vector3(vectorValue.x, 0, 2.5f*vectorValue.z);

            Turn(speed.x);
            
            Monster.SetState(MonsterState.Walk);

            speed.y -= 1000 * Time.deltaTime;

            Controller.Move(speed * Time.deltaTime);
        }

        public void Turn(float direction)
        {
            Monster.transform.localScale = new Vector3(Mathf.Sign(direction)*-0.4f, 0.4f, 1);
        }

        public void PlayerDie()
        {
            PlayerDied = true;
        }

        public void PlayerRevive()
        {
            PlayerDied = false;
            detected = false;
        }

        public void MonsterDie()
        {
            MobDied = true;
        }

        public void Detected()
        {
            detected = true;
        }
    }
}