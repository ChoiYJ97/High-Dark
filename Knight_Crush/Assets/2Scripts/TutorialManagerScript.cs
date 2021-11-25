using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using HeroEditor.Common;

namespace Assets.Origin.Scripts
{
    public class TutorialManagerScript : MonoBehaviour
    {
        static TutorialManagerScript _Uniqueinstance;
        public static TutorialManagerScript _instace
        {
            get { return _Uniqueinstance; }
        }

        public Image Blackout;
        public DialogueAnimation Dialogue;
        public DialogueAnimation_Second Dialogue_s;

        public GameObject[] Scenes;
        public GameObject AdventureStartUIBG;
        public Canvas AdventureStartUICanvas;

        public GameObject Sword;
        public Transform SwordPos;
        public Character Player;
        public Transform PlayerTrans;
        public SpriteGroupEntry item;

        bool Once = false, twice = false;

        void Start()
        {
            _Uniqueinstance = this;

            Dialogue = FindObjectOfType<DialogueAnimation>();
            Dialogue_s = FindObjectOfType<DialogueAnimation_Second>();
            StartCoroutine(SceneChange(0));
            FadeOut();
        }

        float distance;
        bool startGame = false;
        void Update()
        {
            if(Dialogue.currentSceneNum() == 15 && !Once)
            {
                Once = true;
                FadeIn();
                Dialogue.allSetActiveFalse();
            }

            if (Once && !twice)
            {
                twice = true;
                StartCoroutine(SceneChange(1));
            }

            distance = Vector3.Distance(PlayerTrans.position, SwordPos.position);
            if (distance <= 1.5f)
                Dialogue_s.buttonsControl(1);
            if (Input.GetKeyDown(KeyCode.Space)&& distance <= 1.5f)
            {
                StartCoroutine(ThinkingBeforeTakeSword());
            }

            if (startGame && Input.GetKeyDown(KeyCode.Escape))
            {
                AdventureStartUIBG.SetActive(true);
                AdventureStartUICanvasSortingOrder(11);
            }
        }

        public void FadeOut()//화면 밝아지기
        {
            StartCoroutine(FadeOutCoroutine());
        }
        public void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
        }

        IEnumerator FadeOutCoroutine()
        {
            float fadeCount = 1;
            while (fadeCount > 0.0f)
            {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }

        IEnumerator FadeInCoroutine()//화면 어둡게하기
        {
            float fadeCount = 0;
            while (fadeCount < 1.0f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }

        IEnumerator SceneChange(int num)
        {
            switch (num)
            {
                case 0:
                    Scenes[0].SetActive(true);
                    yield return new WaitForSeconds(0f);
                    Scenes[1].SetActive(false);
                    break;
                case 1:
                    yield return new WaitForSeconds(1.5f);
                    Scenes[0].SetActive(false);
                    Scenes[1].SetActive(true);
                    AdventureStartUIControl(0);
                    FadeOut();
                    Dialogue_s.StartScene();
                    break;
            }
        }

        IEnumerator ThinkingBeforeTakeSword()
        {
            Dialogue_s.NextDialogue();
            yield return new WaitForSeconds(2.5f);
            Dialogue_s.NextDialogue();
            Sword.SetActive(false);
            Player.Equip(item, EquipmentPart.MeleeWeapon1H);
            Player.gameObject.AddComponent<PlayerAttack>();
            startGame = true;
        }

        public void AdventureStartUIControl(int TF)
        {
            if (TF == 0)
                AdventureStartUIBG.SetActive(false);

            else if (TF == 1)
                AdventureStartUIBG.SetActive(true);
        }

        public void CallSceneManager()
        {
            StartCoroutine(intervalSceneMonve());
        }

        IEnumerator intervalSceneMonve()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManagerScript._instance.GoTutorialScene();
        }

        public void AdventureStartUICanvasSortingOrder(int num)
        {
            AdventureStartUICanvas.sortingOrder = num;
        }

    }
}