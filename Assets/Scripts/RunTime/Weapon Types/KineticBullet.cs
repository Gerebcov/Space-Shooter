using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticBullet : BaseBullet {
	[SerializeField]
	float armorPiercing = 1;

	public override void Contact (BaseGameObject Object)
	{
		Vector2 deltaVlocity = Rigidbodies[0].velocity - Object.Rigidbodies[0].GetVector(Rigidbodies[0].transform.up);
		float ContactForce = deltaVlocity.magnitude * Rigidbodies[0].mass;
		Object.Rigidbodies[0].AddForce (Rigidbodies[0].transform.up * ContactForce, ForceMode2D.Impulse);
		if (Object.Fraction != fraction)
			Object.AddedDamage (ContactForce * armorPiercing, DamageTypes.Kinetic);
		Destroy (gameObject);
	}

}
