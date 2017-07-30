using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreedomCircle : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		//		Debug.Log ("COLLIDED with " + other.gameObject.tag);
		if (other.gameObject.tag == "Enemy") {
			EnemyProbe enemy = other.gameObject.GetComponentInChildren<EnemyProbe> ();
			if (enemy != null) {
				enemy.FreeBot ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			EnemyProbe enemy = other.gameObject.GetComponentInChildren<EnemyProbe> ();
			if (enemy != null) {
				enemy.EscapingFreedom ();
			}
		}
	}

}
