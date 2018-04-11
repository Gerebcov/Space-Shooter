using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet {
	[SerializeField]
	float acceleration;

	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce (transform.up * acceleration * TimeManager.LevelFrameTime, ForceMode2D.Impulse);
	}
}
