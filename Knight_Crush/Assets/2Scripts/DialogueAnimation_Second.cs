using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class DialogueAnimation_Second : MonoBehaviour
    {
        public GameObject[] Dialogues;
        public TutorialMovementScript PlayerMove;
        public GameObject[] Button;

        bool start = false;
        int sceneNum = 0;

        void Start()
        {
            PlayerMove = FindObjectOfType<TutorialMovementScript>();

            allSetActiveFalse();

            buttonSetActiveFalse();
        }

        void Update()
        {
            if (start)
            {
                if (Input.GetKeyDown(KeyCode.Return) && sceneNum < 3)
                {
                    StartCoroutine(Delay());
                }

                if (sceneNum == 0)
                {
                    Dialogues[0].SetActive(true);
                    PlayerMove.moveStop();
                }
                else if (sceneNum == 1)
                {
                    Dialogues[0].SetActive(false);
                    Dialogues[1].SetActive(true);
                }
                else if (sceneNum == 2)
                {
                    Dialogues[1].SetActive(false);
                    Dialogues[2].SetActive(true);
                }
                else if (sceneNum == 3)
                {
                    Dialogues[2].SetActive(false);
                    PlayerMove.moveStart();
                    buttonsControl(0);
                }
                else if(sceneNum == 4)
                {
                    buttonSetActiveFalse();
                    PlayerMove.moveStop();
                    Dialogues[3].SetActive(true);
                }
                else if(sceneNum >= 5)
                {
                    buttonSetActiveFalse();
                    buttonsControl(2);
                    buttonsControl(3);
                    PlayerMove.moveStart();
                    Dialogues[3].SetActive(false);
                }
            }
        }


        public void allSetActiveFalse()
        {
            foreach (GameObject go in Dialogues)
            {
                go.SetActive(false);
            }
        }

        public void StartScene()
        {
            start = true;
        }

        public void NextDialogue()
        {
            sceneNum++;
        }

        IEnumerator Delay()
        {
            NextDialogue();
            yield return new WaitForSeconds(1.0f);
        }

        public void buttonSetActiveFalse()
        {
            foreach (GameObject go in Button)
                go.SetActive(false);
        }

        public void buttonsControl(int group)
        {
            switch (group)
            {
                case 0:
                    Button[0].SetActive(true);
                    Button[1].SetActive(true);
                    Button[2].SetActive(true);
                    Button[3].SetActive(true);
                    break;
                case 1:
                    Button[4].SetActive(true);
                    break;
                case 2:
                    Button[5].SetActive(true);
                    break;
                case 3:
                    Button[6].SetActive(true);
                    break;
            }
        }
    }
}
