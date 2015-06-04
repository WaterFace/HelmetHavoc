using UnityEngine;
using System.Collections;

public class LevelChange : MonoBehaviour {

    public string nextLevel;
    public AudioClip sound;
    public int level;

    private bool doOnce = true;
    private AudioSource source;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = sound;
        Debug.Log(sound.length, this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        if (doOnce)
        {
            doOnce = false;
            SaveLoad.SetProgress(level);
            Debug.Log("Loading level: " + nextLevel, this);
            source.Play();
            StartCoroutine(DelayLoad());
        }
    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(sound.length);
        Application.LoadLevel(nextLevel);
    }
}
