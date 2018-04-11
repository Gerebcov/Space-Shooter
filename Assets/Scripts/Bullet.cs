using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField]
	float startImpylse;
	protected Rigidbody2D rigidbody2D;
	[SerializeField]
	GameObject boom;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2);
		rigidbody2D = GetComponent<Rigidbody2D> ();
		rigidbody2D.AddForce (transform.up * startImpylse, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D colleder)
	{
		BaseGameObject go = colleder.gameObject.GetComponent<BaseGameObject> ();
		if (go) {
			go.HealsPoint--;
			Destroy (gameObject);
			GameObject b = (GameObject)Instantiate (boom, transform.position, Quaternion.identity);
			Destroy (b, 2);
		}
	}
}
