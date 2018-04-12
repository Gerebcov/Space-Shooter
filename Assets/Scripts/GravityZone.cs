using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {
	[SerializeField]
	List<Rigidbody2D> rigi = new List<Rigidbody2D>();
	[SerializeField]
	float range, mass;

	float GConstans = 0.00667f;

	void Awake()
	{
		range = GetComponent<CircleCollider2D> ().radius;
	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach (Rigidbody2D r in rigi) 
		{
			float sqrDistans = (((Vector2)(transform.position - r.transform.position)).sqrMagnitude / (range * range));
			float forse = ((mass * r.mass) / sqrDistans) * GConstans;
			r.velocity = (r.velocity + (((Vector2)(transform.position - r.transform.position)).normalized * forse * Time.fixedDeltaTime)) / (1 - (r.drag * Time.fixedDeltaTime));
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Rigidbody2D rig = coll.attachedRigidbody;
		if (rig != null)
			rigi.Add (rig);
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		Rigidbody2D rig = coll.attachedRigidbody;
		if (rig != null)
			rigi.Remove (rig);
	}
}
