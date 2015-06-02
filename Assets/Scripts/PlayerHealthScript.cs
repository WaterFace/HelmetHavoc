using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

    public ChargeBarScript healthBar;
    public HealthManager healthManager;
    public Vector2 barPos;
    public GameObject bloodParticle;
  
    private Transform bar;
    private Rigidbody2D rb;

	void Start ()
    {
        healthManager.Reset();
        bar = healthBar.gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        healthBar.SetValue(healthManager.percent);

        var worldPos = Camera.main.ViewportToWorldPoint(barPos);
        bar.position = new Vector3(worldPos.x, worldPos.y, bar.position.z);
	}

    void Damage(float amount)
    {
        healthManager.ModHealth(-amount);
        Debug.Log(string.Format("Hit for {0} damage", amount), this);
        var blood = Instantiate<GameObject>(bloodParticle);
        blood.transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
        Destroy(blood, blood.GetComponent<ParticleSystem>().duration);
    }

    void Kicked(float strength)
    {
        Debug.Log("Kicked with force: " + strength.ToString(), this);
        rb.AddForce(Vector2.right * strength, ForceMode2D.Impulse);
        GetComponent<PlayerMovementScript>().Jump(Mathf.Abs(strength));
    }
}
