﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {
	[SerializeField]
	List<Rigidbody2D> rigi = new List<Rigidbody2D>();
	[SerializeField]
	float range = 0, mass = 0;

	float GConstans = 0.00667f;

	void Awake()
	{
		range = GetComponent<CircleCollider2D> ().radius;
	}

	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < rigi.Count; i++) {
			if (rigi[i] != null) {
				float sqrDistans = (((Vector2)(transform.position - rigi[i].transform.position)).sqrMagnitude / (range * range));
				float forse = ((mass * rigi[i].mass) / sqrDistans) * GConstans;
				rigi[i].velocity += ((Vector2)(transform.position - rigi[i].transform.position)).normalized * forse * TimeManager.LevelFixedDeltaTime;
			} else {
				rigi.RemoveAt (i);
				i--;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Rigidbody2D rig = coll.attachedRigidbody;
		if (rig != null && rig.bodyType != RigidbodyType2D.Kinematic && !GetDuplicate(rig))
			rigi.Add (rig);
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		Rigidbody2D rig = coll.attachedRigidbody;
		if (rig != null && rig.bodyType != RigidbodyType2D.Kinematic)
			rigi.Remove (rig);
	}

	bool GetDuplicate(Rigidbody2D Rigidbody)
	{
		foreach (Rigidbody2D R in rigi) 
		{
			if (R == Rigidbody)
				return true;
		}
		return false;
	}
}
