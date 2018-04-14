using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticBullet : BaseBullet {

	Rigidbody2D Rigidbody;

	void Awake()
	{
		Rigidbody = GetComponent<Rigidbody2D> ();
	}

	public override void Contact (BaseGameObject Object)
	{
		Vector2 deltaVlocity = Rigidbody.velocity - Object.Rigidbody.velocity;
		Damage = deltaVlocity.magnitude * Rigidbody.mass;
		Object.Rigidbody.AddForce (deltaVlocity.normalized * Damage, ForceMode2D.Impulse);
	}

}
