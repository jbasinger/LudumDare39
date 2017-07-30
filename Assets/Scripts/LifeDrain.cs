using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDrain : MonoBehaviour {

	public float lifePerSecond;
	public HitPoints hitPoints;
	public float maxAlive;
	public float currentAlive;

	public Image aliveBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		hitPoints.ApplyDamage (lifePerSecond * Time.deltaTime * ((maxAlive-currentAlive)/maxAlive));
		aliveBar.fillAmount = currentAlive / maxAlive;
	}
}
