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

	[SerializeField]
	protected Fractions fraction;

	[SerializeField]
	int parentInstanceID = 0;

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

	public void SetFraction(Fractions Fraction, int UnitID)
	{
		fraction = Fraction;
		parentInstanceID = UnitID;
	}

	public void SetStartVelosity(float StartAcceleration, Vector2 StartVelosity)
	{
		startVelosity = StartVelosity;
		startAcceleration = StartAcceleration;
	}

	public virtual void Contact(BaseGameObject Object)
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.isTrigger)
			return;
		BaseGameObject contactObject = collider.GetComponent<BaseGameObject>();
		if (contactObject == null && collider.attachedRigidbody != null)
			contactObject = collider.attachedRigidbody.GetComponent<BaseGameObject> ();
		if (contactObject != null && contactObject.ID != parentInstanceID)
			Contact (contactObject);
	}
}
