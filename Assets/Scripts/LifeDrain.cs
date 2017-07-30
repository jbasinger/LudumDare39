using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrain : MonoBehaviour {

	public float lifePerSecond;
	public HitPoints hitPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		hitPoints.ApplyDamage (lifePerSecond * Time.deltaTime);
	}
}
