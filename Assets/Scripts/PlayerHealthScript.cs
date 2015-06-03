using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

    public ChargeBarScript healthBar;
    public HealthManager healthManager;
    public Vector2 barPos;
    public GameObject bloodParticle;
  
    private Transform bar;
    private Rigidbody2D rb;
    private PlayerAudio audio;

	void Start ()
    {
        healthManager.Reset();
        bar = healthBar.gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<PlayerAudio>();
	}
	
	void Update () 
    {
        healthBar.SetValue(healthManager.percent);

        var worldPos = Camera.main.ViewportToWorldPoint(barPos);
        bar.position = new Vector3(worldPos.x, worldPos.y, bar.position.z);
	}

    public void Damage(float amount)
    {
        audio.Hurt(1f);
        healthManager.ModHealth(-amount * GetComponentInChildren<Helmet>().damageTakenMult);
        Debug.Log(string.Format("Hit for {0} damage", -amount * GetComponentInChildren<Helmet>().damageTakenMult), this);
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
