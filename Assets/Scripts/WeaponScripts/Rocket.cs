using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ExplosionBullet {

	[SerializeField]
	Rigidbody2D[] Rigidbodys; 
	public float startImpulse;
	public Vector3 startVelosity;

	void Start () {
		foreach (Rigidbody2D R in Rigidbodys) {
			R.velocity = startVelosity;
			R.AddForce (startImpulse * R.transform.up, ForceMode2D.Impulse);
		}
	}
}
