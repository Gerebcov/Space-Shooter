using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : KineticBullet {

	public Vector2 StartVelocity;
	public float StartImpulse;


	void Start ()
	{
		Rigidbody.velocity = StartVelocity;
		Rigidbody.AddForce (transform.up * StartImpulse, ForceMode2D.Impulse);
	}

}
