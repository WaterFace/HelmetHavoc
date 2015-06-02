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
	}
	
	void Update()
    {
        if (menu.paused)
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
            charge += chargeSpeed * Time.deltaTime;
        }

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);
        chargeBar.SetAngle(Mathf.Rad2Deg*Mathf.Atan2(direction.y, direction.x));
        chargeBar.SetValue(charge);

        if (Input.GetButtonUp("Fire1") && !hasJumped)
        {
            rb.AddForce(direction * Mathf.Lerp(minFlySpeed, maxFlySpeed, charge), ForceMode2D.Impulse);
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

        if (!grounded) { checkGround = true; }

        chargeBar.gameObject.SetActive(!(charge <= 0f));
	}

    void Hit(GameObject target)
    {
        target.SendMessage("HitByPlayer", SendMessageOptions.DontRequireReceiver);
        flying = false;

        float t = Unlerp(minFlySpeed, maxFlySpeed, rb.velocity.magnitude);

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
