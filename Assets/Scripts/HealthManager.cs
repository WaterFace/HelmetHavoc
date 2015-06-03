using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

    private float _health;

    public float health { get { return Mathf.Clamp(_health, 0f, maxHealth); } }
    public bool isAlive { get { return health > 0f; } }
    public float percent { get { return health / maxHealth; } }

    public float maxHealth;

    public void Reset()
    {
        _health = maxHealth;
    }

    public void ModHealth(float amount)
    {
        _health = Mathf.Clamp(_health + amount, 0f, maxHealth);
    }
    
}
