using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class MovementScript : MonoBehaviour
    {
        public Character character;
        public CharacterController Controller;
        public HealthSliderScript PlayerHp;

        Vector3 speed;

        bool stop = false;

        void Start()
        {
            if (Controller == null)
            {
                Controller = character.gameObject.AddComponent<CharacterController>();
                Controller.center = new Vector3(0, 1.125f);
                Controller.height = 2.5f;
                Controller.radius = 0.75f;
                Controller.minMoveDistance = 0;
            }

            character.Animator.SetBool("Ready", true);

            PlayerHp = FindObjectOfType<HealthSliderScript>();
        }

        void Update()
        {
            if (stop)
                return;
            if (PlayerHp.currentHp() <= 0.0f)
                return;

            var direction = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -0.7f;
            if (Input.GetKey(KeyCode.RightArrow)) direction.x = 0.7f;
            if (Input.GetKey(KeyCode.UpArrow)) direction.z = 0.7f;
            if (Input.GetKey(KeyCode.DownArrow)) direction.z = -0.7f;

            Move(direction);
        }

        public void Move(Vector3 direction)
        {
            speed = new Vector3(5 * direction.x, 0, 10 * direction.z);
            if (direction != Vector3.zero)
            {
                Turn(speed.x);
            }

            if (direction != Vector3.zero)
                character.SetState(CharacterState.Run);
            else if (character.GetState() < CharacterState.DeathB)
                character.SetState(CharacterState.Idle);

            speed.y -= 1000 * Time.deltaTime;
            Controller.Move(speed * Time.deltaTime);
        }

        public void Turn(float direction)
        {
            character.transform.localScale = new Vector3(Mathf.Sign(direction) * 0.7f, 0.7f, 1);
        }

        public void StopM()
        {
            stop = true;
        }
        public void StartM()
        {
            stop = false;
        }

        public CharacterController CharController()
        {
            return Controller;
        }
    }
}