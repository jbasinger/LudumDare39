using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public float amount;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			HitPoints hp = other.gameObject.GetComponent<HitPoints> ();
			hp.ApplyHealing (amount);
			Destroy (this.gameObject);
		}
	}

}
