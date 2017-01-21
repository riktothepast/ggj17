using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class PowerStriker : MonoBehaviour
    {
        public Transform leftArm;
        public Transform leftHand;

        private SpriteRenderer leftHandSprite;

        private Quaternion orginArmRot;
        private Vector3 handPosition;
        private void Start()
        {
            leftHandSprite = leftHand.GetComponentInChildren<SpriteRenderer>();
            orginArmRot = leftHandSprite.transform.localRotation;
            handPosition = leftHandSprite.transform.localPosition;
        }

        public void HitPosition(Vector3 hitPosition)
        {
            leftHandSprite.transform.position = hitPosition;
            leftHandSprite.transform.parent = null;
        }

        public void ResetPosition()
        {
            leftHandSprite.transform.SetParent(leftHand);
            leftHandSprite.transform.localRotation = Quaternion.identity;
            leftHandSprite.transform.localPosition = Vector3.zero;
        }
    }
}