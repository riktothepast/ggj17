using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class SpriteState : MonoBehaviour
    {
        private static SpriteState spriteState;

        public static SpriteState instance
        {
            get
            {
                if (!spriteState)
                {
                    spriteState = FindObjectOfType(typeof(SpriteState)) as SpriteState;

                    if (!spriteState)
                    {
                        GameObject spriteN = new GameObject("SpriteState");
                        spriteState = spriteN.AddComponent<SpriteState>();
                    }
                    else
                    {

                    }
                }

                return spriteState;
            }
        }

        private List<string> activeDolly;
        private List<string> inactiveDolly;
        private bool intied;

        void Awake()
        {
            if (intied == false)
            {
                Reset();
            }
            intied = true;
        }

        private string GetDolly()
        {
            if(intied == false)
            {
                Reset();
                intied = true;
            }
            if(activeDolly.Count == 0)
            {
                return "none";
            }
            string dol = activeDolly[0];
            activeDolly.RemoveAt(0);
            inactiveDolly.Add(dol);
            return dol;
        }

        public void Reset()
        {
            activeDolly = new List<string>();
            activeDolly.Add("ChibiDolly");
            activeDolly.Add("ChibiLil");
            activeDolly.Add("ChibiKatie");
            activeDolly.Add("ChibiHoney");
            inactiveDolly = new List<string>();
        }

        public SpriteManager GetRandoChibi()
        {
            if (activeDolly.Count > 0)
            {
                SpriteManager spriteManager = GameObject.Instantiate(Resources.Load<SpriteManager>("Characters/" + GetDolly()));
                return spriteManager;
            }
            return null;
        }

        private SpriteManager GetRandoChibiEditor()
        {
            return GetChibi(GetDolly());
        }

        private SpriteManager GetChibi(string chibiName)
        {
            if(chibiName == "none")
            {
                return null;
            }
            SpriteManager spriteManager = GameObject.Instantiate(Resources.Load<SpriteManager>("Characters/ChibiDolly"));
            Sprite[] sprites = Resources.LoadAll<Sprite>("Characters/Sprites/" + chibiName);
            List<SpriteRenderer> bodyParts = spriteManager.GetBodyParts();
            for (int i = 0; i < bodyParts.Count; i++)
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    if (bodyParts[i].name == sprites[j].name)
                    {
                        bodyParts[i].sprite = sprites[j];
                        break;
                    }
                }
            }

            return spriteManager;
        }

    }
}