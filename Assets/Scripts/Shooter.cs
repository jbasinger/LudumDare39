using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public GameObject bulletPrefab;
	public float bulletSpeed;
	public float randomOffset;

	public void Shoot(Vector3 at, float damage, Collider2D ignoreCollider = null){
		
		at.z = -10;
		at.x += randomOffset * Random.Range(-1f,1f);
		at.y += randomOffset * Random.Range(-1f,1f);

		GameObject bullet = Instantiate (bulletPrefab, this.transform.position, Quaternion.identity, this.transform);
		Quaternion q = Quaternion.LookRotation (transform.position - at, transform.forward);

		bullet.transform.rotation = q;
		bullet.transform.eulerAngles = new Vector3 (0, 0, bullet.transform.eulerAngles.z+90);
//		bullet.transform.eulerAngles = new Vector3 (0, 0, bullet.transform.eulerAngles.z);

//		bullet.transform.parent = this.transform;

		Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D> ();
		bulletBody.velocity = bulletSpeed * bullet.transform.right;

		bullet.GetComponent<Bullet> ().damage = damage;

		if (ignoreCollider != null) {
			Physics2D.IgnoreCollision (bullet.GetComponent<Collider2D> (), ignoreCollider);
		}

	}

}
