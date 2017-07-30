using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {

		string tag = coll.gameObject.tag;

		if (tag == "Enemy" || tag == "Player") {
			HitPoints hp = coll.gameObject.GetComponent<HitPoints> ();
			hp.ApplyDamage (damage);
		}

		Destroy (this.gameObject);
//
//		if (tag != "Player") {
//			
//		}

	}
}
