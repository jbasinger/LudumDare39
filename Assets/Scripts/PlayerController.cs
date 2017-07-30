using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float bulletDamage = 1;
	public float bulletCost = 5f;
	public float freedomCost = 2.5f;
	public float speed = 10f;
	public float bulletCooldownTime = 0.3f;
	public float freedomGrowthSpeed = 2f;
	public GameObject freedomCircle;

	private Rigidbody2D rbody;
	private Collider2D col2D;
	private Shooter shooter;
	private HitPoints hp;
	private float bulletCooldown;
	private bool freeingBot;
	private GameObject currentCircle;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		col2D = GetComponent<Collider2D> ();
		shooter = GetComponent<Shooter> ();
		hp = GetComponent<HitPoints> ();
		freeingBot = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		rbody.velocity = move * speed;

		//0==left 1 == right 2 == middle
		if (Input.GetMouseButton (0) && bulletCooldown <= 0 && !freeingBot) {
			shooter.Shoot (Camera.main.ScreenToWorldPoint (Input.mousePosition), bulletDamage, col2D);
			bulletCooldown += bulletCooldownTime;
			hp.ApplyDamage (bulletCost);
		}

		if (freeingBot) {
			hp.ApplyDamage (freedomCost * Time.deltaTime);
			if (currentCircle != null && currentCircle.transform.localScale.x < 2.5f) {
				currentCircle.transform.localScale = new Vector3 (currentCircle.transform.localScale.x + freedomGrowthSpeed * Time.deltaTime, currentCircle.transform.localScale.x + freedomGrowthSpeed * Time.deltaTime, currentCircle.transform.localScale.z);
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			freeingBot = true;
			currentCircle = Instantiate (freedomCircle, this.transform.position, Quaternion.identity, this.transform);
		}

		if (Input.GetMouseButtonUp (1)) {
			freeingBot = false;
			Destroy (currentCircle);
		}

		if (bulletCooldown > 0) {
			bulletCooldown -= Time.deltaTime;
		}

	}

}
