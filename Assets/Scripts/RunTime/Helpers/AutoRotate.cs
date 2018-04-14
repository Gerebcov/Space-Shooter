using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

	[SerializeField]
	Vector3 rotateVector;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotateVector * TimeManager.LevelFrameTime);
	}
}
