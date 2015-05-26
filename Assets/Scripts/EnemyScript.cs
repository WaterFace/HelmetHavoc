using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    private HealthManager healthManager;
    private GameObject sprite;

	void Start () 
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.Reset();
	}

    void HitByPlayer()
    {
        healthManager.ModHealth(-10f);

        transform.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, healthManager.percent);

        if (!healthManager.isAlive)
        {
            healthManager.ModHealth(-10f);

            var rb = GetComponent<Rigidbody2D>();
            rb.fixedAngle = false;
            rb.AddTorque(Random.Range(-30f, 30f));
            rb.AddForce(new Vector2(0f, Random.Range(2f, 6f)), ForceMode2D.Impulse);

            transform.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }
    }
}
