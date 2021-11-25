using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class MonsterHpBarScript : MonoBehaviour
    {
        public MonsterHitted MHp;
        public Image HpBar;

        bool once = false;
        float timecheck = 0;

        float curhp = 1;
        float Totalhp = 1;
        
        void Start()
        {
            initHpbarSize();
        }

        void Update()
        {
            timecheck += Time.deltaTime;
            if(timecheck >= 0.01f && !once)
            {
                Totalhp = MHp.TotalHp();
                once = true;
            }
            curhp = MHp.curHp();

            HpBarUpdate();
        }

        public void HpBarUpdate()
        {
            if(curhp <= 0)
            {
                HpBar.rectTransform.localScale = new Vector3(0, 1f, 1f);
            }
            HpBar.rectTransform.localScale = new Vector3(curhp / Totalhp, 1f, 1f);
        }

        void initHpbarSize()
        {
            HpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}