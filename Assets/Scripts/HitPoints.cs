using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HitPoints : MonoBehaviour {

	public int maxHitpoints;
	public int currentHitPoints;

	// Use this for initialization
	void Start () {
		MaxHealing ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeath ();
	}

	public void ApplyDamage(int value){
		currentHitPoints -= value;
		CheckMaxHitPoints ();
		CheckDeath ();
	}

	public void ApplyHealing(int value){
		currentHitPoints += value;
		CheckMaxHitPoints ();
		CheckDeath ();
	}

	public void MaxHealing(){
		currentHitPoints = maxHitpoints;
	}

	void CheckDeath(){
		if (currentHitPoints <= 0) {
			Destroy (this.gameObject);
		}
	}

	void CheckMaxHitPoints(){
		if (currentHitPoints > maxHitpoints) {
			currentHitPoints = maxHitpoints;
		}
	}

}
