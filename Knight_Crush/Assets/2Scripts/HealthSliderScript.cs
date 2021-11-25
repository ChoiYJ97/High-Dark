using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class HealthSliderScript : MonoBehaviour
    {
        public Slider HpSlider;
        public PlayerStatus Pstatus;
        public Text HpText;
        public Text TotalHpText;

        float timecheck = 0;
        bool Once = false;

        void Start()
        {
            HpSlider = gameObject.GetComponent<Slider>();
            Pstatus = FindObjectOfType<PlayerStatus>();
            HpSlider.value = 1;
        }

        void Update()
        {
            timecheck += Time.deltaTime;
            if(timecheck >= 0.4f && !Once)
            {
                Set_Total_Hp_Value();
                Once = true;
            }
            UpdateText();
        }

        public void Set_Total_Hp_Value()
        {
            string slash = "/";
            HpSlider.maxValue = Pstatus.getStatus(2);
            HpSlider.value = HpSlider.maxValue;
            HpText.text = HpSlider.maxValue.ToString();
            TotalHpText.text = slash + HpSlider.maxValue.ToString();
        }

        public void UpdateSlider(int type, float value)
        {
            switch (type)
            {
                case 0:
                    HpSlider.value -= value;
                    HpText.text = HpSlider.value.ToString();
                    break;
                case 1:
                    HpSlider.value += value;
                    HpText.text = HpSlider.value.ToString();
                    break;
            }
        }

        public float currentHp()
        {
            return HpSlider.value;
        }

        public void FullHealth()
        {
            HpSlider.value = HpSlider.maxValue;
        }

        public void UpdateText()
        {
            HpText.text = HpSlider.value.ToString();
        }
    }
}
