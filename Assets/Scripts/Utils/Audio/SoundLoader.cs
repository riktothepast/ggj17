using UnityEngine;
using System.Collections;

public class SoundLoader : MonoBehaviour {
    [SerializeField]
    private AudioClip sound;

    public bool PlayOnStart;

    void Start()
    {
        if (PlayOnStart)
        {
            PlaySound();
        }
    }

	public void PlaySound () {
        AudioManager.Instance.PlaySound(sound);
	}

}