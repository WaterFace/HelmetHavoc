using UnityEngine;
using System.Collections;

public class PlayerJetpackScript : MonoBehaviour {

    private Vector2 mouseWorldPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
    private Vector2 direction { get { return ((Vector3)mouseWorldPos - transform.position).normalized; } }
    private Rigidbody2D rb;
    private float charge;
    private Animator anim;
    private bool prevGrounded;
    private bool flying;
    private bool hasJumped = false;
    private PlayerAudio audio;
    private bool charging;

    [Header("References")]
    public Transform groundCheck;
    public GameObject jumpParticle;
    public GameObject jumpTrail;
    public ChargeBarScript chargeBar;
    public CameraShake camera;
    public PauseMenu menu;

    [Header("Charge Values")]
    public float minFlySpeed;
    public float maxFlySpeed;
    public float chargeSpeed;
    public float minHitDamage;
    public float maxHitDamage;

    [Header("Camera Shake")]
    public float minShake;
    public float maxShake;
    public float minShakeDuration;
    public float maxShakeDuration;
    
    internal bool grounded;
    internal bool checkGround;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<PlayerAudio>();
	}
	
	void Update()
    {
        if (Time.timeScale == 0f || !GetComponent<HealthManager>().isAlive)
        {
            return;
        }

        RaycastHit2D hit;
        if (checkGround)
        {
            hit = Physics2D.Linecast(transform.position, groundCheck.position);
            grounded = hit;
            if (hit) { hasJumped = false; }
            anim.SetBool("Land", grounded);
        }
        else
        {
            grounded = false;
        }

        if (grounded && (!prevGrounded) && flying)
        {
            Hit(hit.transform.gameObject);
        }

        if (grounded && !prevGrounded) { flying = false; }

        //anim.SetBool("Land", grounded && !prevGrounded);

        prevGrounded = grounded;

        if (Input.GetButton("Fire1") && !hasJumped)
        {
            if (charging == false)
            {
                audio.StartCharge();
            }

            charging = true;
            charge += chargeSpeed * Time.deltaTime;
        }

        //Mathf.Log10(charge) / 2f + 5f / 3f * Mathf.Log10(charge + 1f) + 0.5f, 0f, 1f

        var y = -Mathf.Pow(2f, -5f * charge) + 1.031f; //trust me 

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);
        chargeBar.SetAngle(Mathf.Rad2Deg*Mathf.Atan2(direction.y, direction.x));
        chargeBar.SetValue(y);

        if (Input.GetButtonUp("Fire1") && !hasJumped)
        {
            audio.Launch(1f);
            audio.StopCharge();
            charging = false;
            rb.AddForce(direction * Mathf.Lerp(minFlySpeed, maxFlySpeed, y), ForceMode2D.Impulse);
            charge = 0f;
            checkGround = false;

            anim.SetTrigger("Fly");
            anim.SetBool("Land", false);
            flying = true;
            hasJumped = true;

            var ps = Instantiate<GameObject>(jumpParticle);
            ps.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 2f);
            Destroy(ps, ps.GetComponent<ParticleSystem>().duration);
        }

        if (y >= 1f)
        {
            GetComponent<PlayerHealthScript>().Damage(30f);
            charge = 0f;
            audio.Explode(1f);
            charging = false;

            var ps = Instantiate<GameObject>(jumpParticle);
            ps.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - 2f);
            ps.GetComponent<ParticleSystem>().startColor = new Color(1f, 0.313725f, 0.04f);
            Destroy(ps, ps.GetComponent<ParticleSystem>().duration);
        }

        if (!grounded) { checkGround = true; }

        chargeBar.gameObject.SetActive(!(charge <= 0f));
	}

    void Hit(GameObject target)
    {
        audio.Impact(1f);

        float t = Unlerp(minFlySpeed, maxFlySpeed, rb.velocity.magnitude);

        target.SendMessage("HitByPlayer", Mathf.Lerp(minHitDamage, maxHitDamage, t) * GetComponentInChildren<Helmet>().damageMult, SendMessageOptions.DontRequireReceiver);
        Debug.Log(string.Format("Dealt {0} damage", Mathf.Lerp(minHitDamage, maxHitDamage, t) * GetComponentInChildren<Helmet>().damageMult), this);
        flying = false;
        
        camera.StartShaking(Mathf.Lerp(minShakeDuration, maxShakeDuration, t),
                            Mathf.Lerp(minShake, maxShake, t));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (flying) { Hit(other.gameObject); }
    }

    float Unlerp(float from, float to, float value)
    {
        value = Mathf.Clamp(value, from, to);
        return (value - from) / (to - from);
    }
}
