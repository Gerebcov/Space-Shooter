using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoDestroy))]
public class BaseBullet : MonoBehaviour {

	public float Damage;
	public Fractions fraction = Fractions.Environment;

	public virtual void Contact(BaseGameObject Object)
	{
		if(Object.Fraction != fraction)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		BaseGameObject contactObject = collider.GetComponent<BaseGameObject>();
		if (collider.attachedRigidbody != null && contactObject != null)
			Contact (contactObject);
	}
}
