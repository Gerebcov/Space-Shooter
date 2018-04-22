using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseGameObject {

	[SerializeField]
	ParticleSystem particleContact;

	public void SetUnitParameters(Rigidbody2D[] rigidbody)
	{
		Rigidbodies = rigidbody;
	}

	public void SetFraction(Fractions fraction)
	{
		Fraction = fraction;
	}

	public override void Dead ()
	{
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		
	}

}
