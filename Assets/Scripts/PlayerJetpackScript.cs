using UnityEngine;
using System.Collections;

public class PlayerJetpackScript : MonoBehaviour {

    private Vector2 mouseWorldPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
    private Vector2 direction { get { return ((Vector3)mouseWorldPos - transform.position).normalized; } }
    private bool ignoreGround = false;
    private Rigidbody2D rb;
    private int blockingMask;
    private float charge;
    
    public Transform groundCheck;
    public ChargeBarScript chargeBar;
    public PlayerMovementScript player;
    public float minFlySpeed;
    public float maxFlySpeed;
    public float chargeSpeed;
    
    internal bool grounded;
    internal bool checkGround;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blockingMask = 1 << LayerMask.NameToLayer("Ground");
	}
	
	void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, blockingMask) && checkGround;
        if (!grounded) { checkGround = true; }

        if (Input.GetButton("Fire1"))
        {
            charge += chargeSpeed * Time.deltaTime;
        }

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);
        chargeBar.SetAngle(Mathf.Rad2Deg*Mathf.Atan2(direction.y, direction.x));
        chargeBar.SetValue(charge);

        if (Input.GetButtonUp("Fire1"))
        {
            rb.AddForce(direction * Mathf.Lerp(minFlySpeed, maxFlySpeed, charge), ForceMode2D.Impulse); //Mathf.Lerp(minFlySpeed, maxFlySpeed, charge)
            charge = 0f;
            checkGround = false;
            grounded = false;
        }

        chargeBar.gameObject.SetActive(!(charge <= 0f));
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, ((Vector3)mouseWorldPos - transform.position).normalized * 3f);
    }
}
