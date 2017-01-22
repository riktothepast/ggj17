using UnityEngine;
using System.Collections;

public class ParticlePooler : Singleton<ParticlePooler> {
    [SerializeField]
    private ParticleSystem lava, outOfScreen;

    protected override void Awake()
    {
        base.Awake();
        lava = Instantiate(lava);
    }

    public void CreateFallingToLava(Vector2 position)
    {
        lava.transform.position = position;
        lava.Play();
    }

    public void CreateOutOfScreen(Vector2 position)
    {
        outOfScreen.transform.position = position;
        outOfScreen.Play();
    }

    IEnumerator Recycle(ParticleSystem obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        obj.Recycle();
    }
}
