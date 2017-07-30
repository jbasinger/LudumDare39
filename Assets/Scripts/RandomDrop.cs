using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour {

	public GameObject[] dropList;
	[Range(0,1)]
	public float dropChance;

	public void Drop(){
		if (Random.value <= dropChance) {
			Instantiate (dropList [Random.Range (0, dropList.Length)], this.transform.position, Quaternion.identity, this.transform.parent);
		}
	}
}
