using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    public AudioClip scream;
    
    private HealthManager health;
    private bool doOnce = true;

    // Use this for initialization
    void Start()
    {
        health = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isAlive)
        {
            doOnce = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        AudioSource.PlayClipAtPoint(scream, transform.position);
        yield return new WaitForSeconds(scream.length);
        Application.LoadLevel("YouWin");
    }
}
