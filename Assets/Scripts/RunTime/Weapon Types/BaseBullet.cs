using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoDestroy))]
public class BaseBullet : MonoBehaviour {
	
	[SerializeField]
	float startAcceleration = 0;
	[SerializeField]
	Vector2 startVelosity = Vector2.zero;
	[SerializeField]
	protected Rigidbody2D[] Rigidbodies;

	Fractions fraction;

	void Start()
	{
		InitializationBullet ();
	}

	public virtual void InitializationBullet ()
	{
		foreach (Rigidbody2D R in Rigidbodies) {
			R.velocity = startVelosity + ((Vector2)R.transform.up * startAcceleration);
		}
	}

	public void SetFraction(Fractions Fraction)
	{
		fraction = Fraction;
	}

	public void SetStartVelosity(Vector2 StartVelosity)
	{
		startVelosity = StartVelosity;
	}

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
