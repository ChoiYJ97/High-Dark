using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class TutorialButtonIconScript : MonoBehaviour
    {
        public Image UpKey;
        public Image LeftKey;
        public Image DownKey;
        public Image RightKey;
        public Image SpaceKey;
        public Image CtrlKey;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            KeyboardIcons();
        }

        void KeyboardIcons()
        {
            if (Input.GetKey(KeyCode.UpArrow))
                UpKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                UpKey.color = new Color(1f, 1f, 1f);

            if (Input.GetKey(KeyCode.LeftArrow))
                LeftKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                LeftKey.color = new Color(1f, 1f, 1f);

            if (Input.GetKey(KeyCode.DownArrow))
                DownKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                DownKey.color = new Color(1f, 1f, 1f);

            if (Input.GetKey(KeyCode.RightArrow))
                RightKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                RightKey.color = new Color(1f, 1f, 1f);

            if (Input.GetKey(KeyCode.Space))
                SpaceKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                SpaceKey.color = new Color(1f, 1f, 1f);

            if (Input.GetKey(KeyCode.LeftControl))
                CtrlKey.color = new Color(0.6f, 0.6f, 0.6f);
            else
                CtrlKey.color = new Color(1f, 1f, 1f);
        }

    }
}