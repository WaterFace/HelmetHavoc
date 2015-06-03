using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    public float healAmount;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var health = other.gameObject.GetComponent<HealthManager>();

            if (health.percent < 1f)
            {
                health.ModHealth(healAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
