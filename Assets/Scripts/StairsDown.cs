using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDown : MonoBehaviour {

	public GameObject levelBuilder;

	void OnTriggerEnter2D(Collider2D other){
		//		Debug.Log ("COLLIDED with " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			EnvironmentBuilder builder = levelBuilder.GetComponent<EnvironmentBuilder> ();
			builder.NextLevel ();
			Destroy (this.gameObject);
		}
	}

}
