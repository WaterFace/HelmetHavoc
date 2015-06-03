using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyScript : MonoBehaviour {

    private HealthManager healthManager;
    private State state;
    private float timeToFlip;
    private Animator anim;
    private Rigidbody2D rb;
    private bool facingLeft = true;
    private bool grounded;
    private EnemySound audio;

    [HideInInspector] public Transform player;

    public Transform groundCheck;
    
    [Header("Combat")]
    public float kickDamage;
    public float kickForce;

    [Header("AI")]
    public float aggroDistance;
    public float followDistance;

    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public float groundDistance;

    [Header("Details")]
    public float minFlipTime;
    public float maxFlipTime;
    public Color baseColour;

    public enum State
    {
        IDLE,
        SEEKING,
        DEAD
    }

	void Start () 
    {
        audio = GetComponent<EnemySound>();

        healthManager = GetComponent<HealthManager>();
        healthManager.Reset();

        transform.GetComponentInChildren<SpriteRenderer>().color = baseColour;

        transform.GetComponentInChildren<KickHitbox>().kickDamage = kickDamage;

        timeToFlip = Mathf.Clamp(1.0f, minFlipTime, maxFlipTime);

        state = State.IDLE;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}



    void FixedUpdate()
    {
        if (state != State.DEAD)
        {
            transform.GetComponentInChildren<SpriteRenderer>().color = baseColour * Color.Lerp(Color.red,
                                                                                               Color.white,
                                                                                               healthManager.percent);
        }

        grounded = Physics2D.Linecast(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);

        if (Mathf.Abs(rb.velocity.x) < 0.01f)
        {
            anim.SetInteger("HorizontalVelocity", 0);
        }
        else
        {
            anim.SetInteger("HorizontalVelocity", (int)Mathf.Sign(rb.velocity.x));
        }
        anim.SetBool("isAlive", healthManager.isAlive);

        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.position.y - transform.position.y);

        switch (state)
        {
            case State.IDLE:
                if (horizontalDistance < aggroDistance)
                {
                    state = State.SEEKING;
                    break;
                }

                timeToFlip -= Time.deltaTime;
                if (timeToFlip <= 0f)
                {
                    anim.SetTrigger("Flip");
                    timeToFlip = Random.Range(minFlipTime, maxFlipTime);
                    facingLeft = !facingLeft;
                }
                break;
            case State.SEEKING:
                if (horizontalDistance > 1.5f * aggroDistance)
                {
                    state = State.IDLE;
                    break;
                }
                if (horizontalDistance > followDistance)
                {
                    rb.AddForce(new Vector2(player.transform.position.x - transform.position.x, 0f).normalized * speed * Time.deltaTime, ForceMode2D.Impulse);
                }
                if (horizontalDistance < followDistance)
                {
                    if (verticalDistance < 0.75f)
                    {
                        anim.SetTrigger("Kick");
                        anim.SetInteger("HorizontalVelocity", (int)Mathf.Sign(player.position.x - transform.position.x));
                    }
                    else if (grounded)
                    {
                        //int jumpLeftState = Animator.StringToHash("EnemyJumpLeft");
                        //int jumpRightState = Animator.StringToHash("EnemyJumpRight");
                        //anim.SetTrigger("Jump");
                        //if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpLeftState || anim.GetCurrentAnimatorStateInfo(0).fullPathHash == jumpRightState)
                        //{
                        //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        //}
                    }
                }

                break;
            case State.DEAD:
                break;
        }

        anim.SetBool("Left", facingLeft);
    }

    void HitByPlayer(float damage)
    {
        audio.Hurt(1f);

        healthManager.ModHealth(-damage);
        transform.GetComponentInChildren<SpriteRenderer>().color = baseColour * Color.Lerp(Color.red, Color.white, healthManager.percent);

        if (!healthManager.isAlive)
        {
            state = State.DEAD;

            healthManager.ModHealth(-damage);

            var rb = GetComponent<Rigidbody2D>();
            rb.fixedAngle = false;
            rb.AddTorque(Random.Range(-30f, 30f));
            rb.AddForce(new Vector2(0f, Random.Range(2f, 6f)), ForceMode2D.Impulse);

            transform.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
    }
}
