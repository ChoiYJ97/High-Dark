using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

namespace Assets.Origin.Scripts
{
    public class PlayerHitted : MonoBehaviour
    {
        public Monster Monster;
        public Character Character;

        public GameObject DamageTxt;
        public HealthSliderScript PlayerHp;
        MonsterMovingScript MMoving;

        void Start()
        {
            Monster = this.gameObject.GetComponent<Monster>();
            Character = FindObjectOfType<Character>();

            if (Monster != null)
            {
                Monster.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            }
            PlayerHp = FindObjectOfType<HealthSliderScript>();

            MMoving = this.gameObject.GetComponent<MonsterMovingScript>();
        }

        void Update()
        {
            if (PlayerHp.currentHp() <= 0.0f)
            {
                Character.SetState(CharacterState.DeathB);
                Character.SetExpression("Dead");
            }

        }
        public void OnDestroy()
        {
            if (Monster != null)
            {
                Monster.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if (eventName == "Attack" && Vector3.Distance(transform.position, Character.transform.position) < 1.5f)
            {
                Vector3 y = this.transform.position + new Vector3(Random.Range(-0.7f, 0.7f), 2 + Random.Range(0, 0.3f), 0);
                Instantiate(DamageTxt, y, Quaternion.identity);
                Character.GetComponent<Character>().Spring();

                if (PlayerHp.currentHp() <= 0.0f)
                {
                    MMoving.PlayerDie();
                    
                    return;
                }
            }
        }

    }
}

