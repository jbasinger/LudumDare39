using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyProbe : MonoBehaviour {

	public float forgetCooldownTime;
	public float speed;
	public float range;
	public float bulletCooldownTime = 0.5f;

	private GameObject aggro;
	private float forgetCooldown;
	private Level level;
	private Transform myTransform;
	private Shooter shooter;
	private Collider2D col2D;
	private float bulletCooldown;

	// Use this for initialization
	void Start () {

		level = EnvironmentBuilder.level;
		if (transform.parent != null) {
			myTransform = transform.parent;
		} else {
			myTransform = transform;
		}

		shooter = myTransform.GetComponent<Shooter> ();
		col2D = myTransform.GetComponent<Collider2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (bulletCooldown > 0) {
			bulletCooldown -= Time.deltaTime;
		}

		if (forgetCooldown > 0) {
			forgetCooldown -= Time.deltaTime;
		}

		if (forgetCooldown <= 0) {
			aggro = null;
		}

		if (aggro != null) {

			float aggroDistance = Vector3.Distance (aggro.transform.position, myTransform.position);
			if ( aggroDistance > range) {

//				Physics2D.Raycast(myTransform.transform,
//				Ray2D ray = new Ray2D(myTransform.position,aggro.transform.position-myTransform.position);
				Debug.DrawRay (myTransform.position, aggro.transform.position - myTransform.position);

				RaycastHit2D hit = Physics2D.Raycast (myTransform.position, aggro.transform.position - myTransform.position, aggroDistance + range);

				if (hit != null && hit.collider != null) {
					if (hit.collider.tag == "Player") {
						myTransform.position = Vector3.MoveTowards (myTransform.position, aggro.transform.position, speed * Time.deltaTime);
						return;
					}
				}

				FindBestGeneralDirection ();


			} else {

				Debug.DrawRay (myTransform.position, aggro.transform.position - myTransform.position);

				RaycastHit2D hit = Physics2D.Raycast (myTransform.position, aggro.transform.position - myTransform.position, aggroDistance + range);

				if (hit != null && hit.collider != null) {
					if (hit.collider.tag == "NotPassable") {
						FindBestGeneralDirection ();
						return;
					}
				}

				if (bulletCooldown <= 0) {
					Attack ();
				}

			}

		}

	}

	void Attack(){
		shooter.Shoot (aggro.transform.position, col2D);
		bulletCooldown += bulletCooldownTime;
	}

	bool FindBestExitInRoom(){

		//This is kind of buggy because rooms collide
		Debug.Log ("Finding the best exit");
		//Hunt that mothertrucker DOWN;
		Tile t = level.TileAtLocation(myTransform.position);
		Room r = t.room;

		if (r.exits.Count > 0) {
			Tile bestExit = null;
			float bestDistance = float.MaxValue;
			foreach (Tile exit in r.exits) {
				Vector3 exitPos = new Vector3 (exit.x, exit.y);
				float dist = Vector3.Distance (aggro.transform.position, exitPos);
				if (dist < bestDistance) {
					bestExit = exit;
					bestDistance = dist;
				}
			}

			if (bestExit != null && Vector3.Distance (aggro.transform.position, bestExit.GetVector3()) > 2) {
				Debug.Log (String.Format ("Heading to best exit x:{0} y:{1}", bestExit.x, bestExit.y));
				Debug.DrawLine (myTransform.position, bestExit.GetVector3 (),Color.red);
				myTransform.position = Vector3.MoveTowards (myTransform.position, bestExit.GetVector3(), speed * Time.deltaTime);
				return true;
			} else {
				return false;
			}

		} else {
			return false;
		}

	}

	void FindBestGeneralDirection(){
		
		Tile me = level.TileAtLocation(myTransform.position);
		Tile bestTile = null;
		float bestDistance = float.MaxValue;
		List<Tile> adjacentTiles = level.GetEightAdjacentTilesFromPosition (me.x, me.y);

		foreach (Tile t in adjacentTiles) {

			Debug.DrawLine (myTransform.position, t.GetVector3 (), Color.green);

			Vector3 tilePos = new Vector3 (t.x, t.y);
			float dist = Vector3.Distance (aggro.transform.position, tilePos);
			if (!t.isPassable) {
				dist += 1000;
			}

//			foreach (Tile prev in previousTiles) {
//				if (t == prev) {
//					dist += 1000;
//				}
//			}

			if (dist < bestDistance) {
				bestTile = t;
				bestDistance = dist;
			}
		}

		if (bestTile != null) {
//			Debug.Log (String.Format ("MY tile x:{0} y:{1}", me.x, me.y));
//			Debug.Log (String.Format ("Heading to best tile x:{0} y:{1}", bestTile.x, bestTile.y));
			Debug.DrawLine (myTransform.position, bestTile.GetVector3 (),Color.red);
//			previousTiles.Enqueue (bestTile);
			myTransform.position = Vector3.MoveTowards (myTransform.position, bestTile.GetVector3 (), speed * Time.deltaTime);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
//		Debug.Log ("COLLIDED with " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			aggro = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
//		Debug.Log ("Cooling down");
		forgetCooldown += forgetCooldownTime;
	}
}
