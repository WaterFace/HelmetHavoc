using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

    public AudioClip hurt;
    public AudioClip explode;
    public AudioClip charge;
    public AudioClip launch;
    public AudioClip impact;

    private AudioSource source;
    private AudioSource chargeSource;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;

        chargeSource = gameObject.AddComponent<AudioSource>();
        chargeSource.playOnAwake = false;
        chargeSource.clip = charge;
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

    public void Explode(float volume)
    {
        PlaySound(explode, volume);
    }

    public void Charge(float volume)
    {
        PlaySound(charge, volume);
    }

    public void Launch(float volume)
    {
        PlaySound(launch, volume);
    }

    public void Impact(float volume)
    {
        PlaySound(impact, volume);
    }

    public void StartCharge()
    {
        chargeSource.Play();
    }

    public void StopCharge()
    {
        chargeSource.Stop();
    }
}
