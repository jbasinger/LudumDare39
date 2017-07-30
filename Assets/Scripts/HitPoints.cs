using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HitPoints : MonoBehaviour {

	public float maxHitpoints;
	public float currentHitPoints;

	public LifeBar lifeBar;
	public RandomDrop drop;

	// Use this for initialization
	void Start () {
		MaxHealing ();
		UpdateLifeBar ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeath ();
	}

	public void ApplyDamage(float value){
		currentHitPoints -= value;
		CheckMaxHitPoints ();
		CheckDeath ();
		UpdateLifeBar ();
	}

	public void ApplyHealing(float value){
		currentHitPoints += value;
		CheckMaxHitPoints ();
		CheckDeath ();
		UpdateLifeBar ();
	}

	public void MaxHealing(){
		currentHitPoints = maxHitpoints;
	}

	void CheckDeath(){
		if (currentHitPoints <= 0) {
			if (drop != null) {
				drop.Drop ();
			}
			Destroy (this.gameObject);
		}
	}

	void CheckMaxHitPoints(){
		if (currentHitPoints > maxHitpoints) {
			currentHitPoints = maxHitpoints;
		}
	}

	void UpdateLifeBar(){
		if (lifeBar == null)
			return;

		lifeBar.SetBar (currentHitPoints / maxHitpoints);
	}

}
