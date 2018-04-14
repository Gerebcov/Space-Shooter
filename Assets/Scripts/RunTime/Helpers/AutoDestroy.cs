using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	[SerializeField]
	float timeToDestroy = 1f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, timeToDestroy);
	}

}
