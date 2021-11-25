using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;

namespace Assets.Origin.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {
        public Character Character;
        public Transform ArmL;
        public Transform ArmR;
        public KeyCode Attack;

        bool stop = false;

        void Start()
        {
            Character = FindObjectOfType<Character>();
            Attack = KeyCode.LeftControl;
        }

        void Update()
        {
            if (stop) return;

            if (Character.Animator.GetInteger("State") >= (int)CharacterState.DeathB) return;

            switch (Character.WeaponType)
            {
                case WeaponType.Melee1H:
                case WeaponType.Melee2H:
                case WeaponType.MeleePaired:
                    if (Input.GetKeyDown(Attack))
                    {
                        Character.Slash();
                    }
                    break;
            }
        }

        public void StopAct()
        {
            stop = true;
        }
        public void StartAct()
        {
            stop = false;
        }
    }
}
