using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoDestroy))]
public class BaseBullet : MonoBehaviour {

	public enum BulledTypes
	{
		Kinetic,
		Explosion,
		Energy,
		Laser
	}

	public float Damage;

	public virtual void Contact(BaseGameObject Object)
	{
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		BaseGameObject contactObject = collider.GetComponent<BaseGameObject>();
		if (collider.attachedRigidbody != null && contactObject != null)
			Contact (contactObject);
	}
}
