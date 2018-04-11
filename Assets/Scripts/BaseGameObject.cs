using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObject : MonoBehaviour {

	[SerializeField]
	bool imortal;
	[SerializeField]
	float maxHealsPoint;
	float healsPoint;
	public float HealsPoint{
		get{ return healsPoint;} 
		set{ if (!imortal) {
				healsPoint = value;
				if (healsPoint <= 0)
					Dead ();
			}
		}
	}

	public void Dead()
	{
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		Inicialization ();
	}

	public void Inicialization()
	{
		if (imortal)
			return;
		HealsPoint = maxHealsPoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D collision) {
		float power = collision.relativeVelocity.magnitude;
		HealsPoint -= power / 10;
	}
}
