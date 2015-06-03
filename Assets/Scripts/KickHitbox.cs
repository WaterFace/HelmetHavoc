using UnityEngine;
using System.Collections;

public class KickHitbox : MonoBehaviour {

    internal float kickDamage;
    internal bool canDamage = true;

    private float kickForce;

    void Start()
    {
        kickForce = transform.GetComponentInParent<EnemyScript>().kickForce;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        kickForce = transform.GetComponentInParent<EnemyScript>().kickForce;

        if (other.gameObject.tag == "Player" && canDamage)
        {
            GetComponentInParent<EnemySound>().Impact(1f);

            other.SendMessage("Damage", kickDamage);
            other.SendMessage("Kicked", kickForce * Mathf.Sign(other.transform.position.x - transform.position.x));

            canDamage = false;
        }
    }

    void Update()
    {
        if (!GetComponent<BoxCollider2D>().enabled)
        {
            canDamage = true;
        }
    }
}
