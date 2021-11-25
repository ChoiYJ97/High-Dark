using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Origin.Scripts
{
    public class SpawnMonsterScript : MonoBehaviour
    {
        //0 = UL, 1 = UR, 2 = DR, 3 = DL
        public Transform[] MobPos;
        [Header("Mob List_Stage 1")]
        public GameObject[] Mob;

        float[] ULMax, DRMax;

        void Start()
        {
            initialMaxPosValue();
        }

        void initialMaxPosValue()
        {
            ULMax = new float[3];
            DRMax = new float[3];

            float x, y, z;
            for(int i = 0; i < 2; i++)
            {
                x = MobPos[i].position.x;
                y = MobPos[i].position.y;
                z = MobPos[i].position.z;

                switch (i)
                {
                    case 0:
                        ULMax[0] = x;
                        ULMax[1] = y;
                        ULMax[2] = z;
                        continue;
                    case 1:
                        DRMax[0] = x;
                        DRMax[1] = y;
                        DRMax[2] = z;
                        continue;
                }
            }
        }

        public void Spawn(GameObject Mob)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(ULMax[0], DRMax[0]),
                Random.Range(DRMax[1]+1.5f, ULMax[1]), Random.Range(DRMax[2], ULMax[2]));

            Instantiate(Mob, SpawnPos, Quaternion.identity);
        }

        public GameObject MobData(int level)
        {
            return Mob[level];
        }
    }
}