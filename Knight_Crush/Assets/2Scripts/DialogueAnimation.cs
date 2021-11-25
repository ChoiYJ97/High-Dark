using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;
using UnityEngine.UI;

namespace Assets.Origin.Scripts
{
    public class DialogueAnimation : MonoBehaviour
    {
        public GameObject[] Dialogues;
        public GameObject[] ThinkingBubbles;
        public Image[] IMGcolors;
        public GameObject thinkingBG;
        SpriteRenderer thinkingBGSprite;

        int sceneNum = 0;

        float timecheck = 0;

        bool startThinking = false, thinking = false;

        void Start()
        {
            thinkingBGSprite = thinkingBG.GetComponent<SpriteRenderer>();

            foreach (GameObject go in Dialogues)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in ThinkingBubbles)
            {
                go.SetActive(false);
            }

            Dialogues[sceneNum].SetActive(true);
        }

        void Update()
        {
            if(Input.anyKeyDown && sceneNum <= 9)
            {
                Dialogues[sceneNum].SetActive(false);
                sceneNum++;
                Dialogues[sceneNum].SetActive(true);
            }

            if(sceneNum >= 10 && sceneNum <= 13)
            {
                timecheck += Time.deltaTime;
                if(timecheck >= 1.0f && sceneNum != 13)
                {
                    startThinking = true;
                    timecheck = 0;
                    Dialogues[sceneNum].SetActive(false);
                    sceneNum++;
                    Dialogues[sceneNum].SetActive(true);
                }

                if(sceneNum == 13)
                {
                    if (timecheck >= 1.0f)
                    {
                        Dialogues[sceneNum].SetActive(false);
                        sceneNum = 10;
                        Dialogues[sceneNum].SetActive(true);
                        timecheck = 0;
                    }
                }
            }

            if (startThinking && !thinking)
            {
                StartCoroutine(FadeOutCoroutine());
                speechBGAlpha();
                thinking = true;
                ThinkingBubbles[0].SetActive(true);
            }

            if(Input.anyKeyDown && thinking && sceneNum != 15)
            {
                ThinkingBubbles[0].SetActive(false);

                ThinkingBubbles[1].SetActive(true);
                StartCoroutine(Delay(2.5f));
            }
        }

        IEnumerator FadeOutCoroutine()
        {
            float fadeCount = 0;
            while (fadeCount < 0.454902f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                thinkingBGSprite.color = new Color(0, 0, 0, fadeCount);
            }
        }

        public void speechBGAlpha()
        {
            foreach (Image go in IMGcolors)
            {
                go.color = new Color(0.6188679f, 0.6188679f, 0.6188679f);
            }
        }

        IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            sceneNum = 15;
        }

        public int currentSceneNum()
        {
            return sceneNum;
        }

        public void allSetActiveFalse()
        {
            foreach (GameObject go in Dialogues)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in ThinkingBubbles)
            {
                go.SetActive(false);
            }

            thinkingBG.SetActive(false);
        }
    }
}