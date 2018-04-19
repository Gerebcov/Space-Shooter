using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticBullet : BaseBullet {

	protected Rigidbody2D Rigidbody;

	void Awake()
	{
		Rigidbody = GetComponent<Rigidbody2D> ();
	}

	public override void Contact (BaseGameObject Object)
	{
		Vector2 deltaVlocity = Rigidbody.velocity - Object.Rigidbodies[0].velocity;
		Damage = deltaVlocity.magnitude * Rigidbody.mass;
		Object.Rigidbodies[0].AddForce (deltaVlocity.normalized * Damage, ForceMode2D.Impulse);
	}

}
