using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

public class ShakeManager : MonoBehaviour {
    VectorShakeTween vectorShakeTween;

    void Start () {
        vectorShakeTween = new VectorShakeTween(Camera.main.transform.position);
    }

    public void DoCameraShake(float intensity = 2f)
    {
        vectorShakeTween.shake(intensity);
    }

    public Vector3 GetShakeVector()
    {
        return vectorShakeTween._shakeOffset;
    }
}
