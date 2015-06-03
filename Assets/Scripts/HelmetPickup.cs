using UnityEngine;
using System.Collections;

public class HelmetPickup : MonoBehaviour
{
    public Helmet.HelmetType type;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInChildren<Helmet>().SetHelmet(type);
            Destroy(gameObject);
        }
    }
}
