using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoDestroy))]
public class ExplosionForceSimulator : MonoBehaviour {

	List<Rigidbody2D> Rigidbodys = new List<Rigidbody2D>();
	[SerializeField]
	CircleCollider2D collider = null;
	public float Range;
	float sqrRange;
	public float Force;
	public float Damage;

	void Start()
	{
		collider.radius = Range;
		sqrRange = Range * Range;
	}
	void OnEnable()
	{
		StartCoroutine (Boom ());
	}

	void OnTriggerEnter2D(Collider2D Collider)
	{
		if (Collider.attachedRigidbody != null) {
			Rigidbodys.Add (Collider.attachedRigidbody);
		}
	}

	IEnumerator Boom()
	{
		yield return new WaitForSeconds (0.02f);
		foreach (Rigidbody2D R in Rigidbodys) {
			if (R != null) {
				BaseGameObject go = R.GetComponent<BaseGameObject> ();
				Vector2 vector = R.position - (Vector2)transform.position;
				float sqrDistanse = vector.sqrMagnitude;
				R.AddForce ((1 - Mathf.Min(sqrDistanse / sqrRange, 1)) * vector.normalized * Force, ForceMode2D.Impulse);
				if (go)
					go.AddedDamage ((1 - Mathf.Min(sqrDistanse / sqrRange, 1)) * Damage, DamageTypes.Explosion);
				
			}
			
		}
	}
}
