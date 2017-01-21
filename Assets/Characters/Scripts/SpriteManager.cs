using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class SpriteManager : MonoBehaviour
    {

        private List<SpriteRenderer> bodyParts;
        private Animator animator;

        // Use this for initialization
        private void Start()
        {
            animator = GetComponent<Animator>();
            bodyParts = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        }

        public void ChangeColor(Color color)
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].color = color;
            }
        }

        public void SetRunSpeed(float normalizedRunDirection, float runSpeed)
        {

            animator.SetFloat("NormalRun", Mathf.Abs(normalizedRunDirection));
            if ((int)normalizedRunDirection != 0)
            {
                transform.localScale = new Vector3(normalizedRunDirection, 1, 1);
            }
        }

        public void SetRunSpeed(float normalizedRunDirection, float runSpeed, bool conditional)
        {
            if (conditional)
            {
                animator.SetFloat("NormalRun", Mathf.Abs(normalizedRunDirection));
                if ((int)normalizedRunDirection != 0)
                {
                    transform.localScale = new Vector3(normalizedRunDirection, 1, 1);
                }
            }
        }

        public void PlayJump()
        {
            animator.SetBool("Jumping", true);
        }

        public void Land()
        {
            animator.SetBool("Jumping", false);
        }
    }
}