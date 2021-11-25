using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts {
    public class StatusWindowScript : MonoBehaviour
    {
        public Text Hp;
        public Text HpTotal;
        public Text ATK;
        public Text DEF;
        public Text Power;

        public PlayerStatus Playerstatus;

        int atk, def, power, hp;

        void Start()
        {
            Playerstatus = FindObjectOfType<PlayerStatus>();

            hp = Playerstatus.getStatus(2);
            atk = Playerstatus.getStatus(0);
            def = Playerstatus.getStatus(3);
            power = Playerstatus.Power(2);
        }

        // Update is called once per frame
        void Update()
        {
            hp = Playerstatus.getStatus(2);
            atk = Playerstatus.getStatus(0);
            def = Playerstatus.getStatus(3);
            power = Playerstatus.Power(2);

            Hp.text = hp.ToString();
            HpTotal.text = "/" + hp.ToString();
            ATK.text = atk.ToString();
            DEF.text = def.ToString();
            Power.text = power.ToString();
        }
    }
}