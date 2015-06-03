using UnityEngine;
using System.Collections;

public class EnemySound : MonoBehaviour {

    public AudioClip hurt;
    public AudioClip impact;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void PlaySound(AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
        Debug.Log(string.Format("Playing sound: {0}", clip.name), this);
    }

    public void Hurt(float volume)
    {
        PlaySound(hurt, volume);
    }

    public void Impact(float volume)
    {
        PlaySound(impact, volume);
    }
}
