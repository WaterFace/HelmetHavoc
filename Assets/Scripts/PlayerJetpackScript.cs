using UnityEngine;
using System.Collections;

public class PlayerJetpackScript : MonoBehaviour {

    private Vector2 mouseWorldPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
    private Vector2 direction { get { return ((Vector3)mouseWorldPos - transform.position).normalized; } }
    private bool ignoreGround = false;
    private bool grounded;
    private Rigidbody2D rb;
    private int blockingMask;
    private float charge;
    private bool checkGround;
    
    public Transform groundCheck;
    public ChargeBarScript chargeBar;
    public float minFlySpeed;
    public float maxFlySpeed;
    public float chargeSpeed;
    
    [HideInInspector] public bool flying;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blockingMask = 1 << LayerMask.NameToLayer("Ground");
	}
	
	void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, blockingMask) && checkGround;

        if (Input.GetButton("Fire1"))
        {
            charge += chargeSpeed * Time.deltaTime;
        }

        charge = Mathf.Clamp(charge, 0.0f, 1.0f);
        chargeBar.SetAngle(Mathf.Rad2Deg*Mathf.Atan2(direction.y, direction.x));
        chargeBar.SetValue(charge);

        if (Input.GetButtonUp("Fire1"))
        {
            rb.AddForce(direction * Mathf.Lerp(minFlySpeed, maxFlySpeed, charge), ForceMode2D.Impulse);
            charge = 0f;
        }

        chargeBar.gameObject.SetActive(!(charge <= 0f));
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, ((Vector3)mouseWorldPos - transform.position).normalized * 3f);
    }
}
