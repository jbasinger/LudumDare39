using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
		if (target != null) {
			this.transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			this.transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
		}
	}
}
