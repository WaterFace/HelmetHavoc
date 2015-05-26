using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

    public ChargeBarScript healthBar;
    public HealthManager healthManager;
    public Vector2 barPos;
  
    private Transform bar;

	void Start ()
    {
        healthManager.Reset();
        bar = healthBar.gameObject.transform;
	}
	
	void Update () 
    {
        if (Input.GetButtonDown("Fire2")) { healthManager.ModHealth(-5f); }

        healthBar.SetValue(healthManager.percent);

        var worldPos = Camera.main.ViewportToWorldPoint(barPos);
        bar.position = new Vector3(worldPos.x, worldPos.y, bar.position.z);

	}
}
