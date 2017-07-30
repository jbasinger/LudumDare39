using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	public float bulletCooldownTime = 0.3f;

	private Rigidbody2D rbody;
	private Collider2D col2D;
	private Shooter shooter;
	private float bulletCooldown;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		col2D = GetComponent<Collider2D> ();
		shooter = GetComponent<Shooter> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		rbody.velocity = move * speed;

		//0==left 1 == right 2 == middle
		if (Input.GetMouseButton (0) && bulletCooldown <= 0) {
			shooter.Shoot (Camera.main.ScreenToWorldPoint (Input.mousePosition), col2D);
			bulletCooldown += bulletCooldownTime;
		}

		if (bulletCooldown > 0) {
			bulletCooldown -= Time.deltaTime;
		}

	}
}
