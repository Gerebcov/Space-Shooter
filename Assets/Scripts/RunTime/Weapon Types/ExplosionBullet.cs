using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : BaseBullet {

	[SerializeField]
	GameObject ExplosionForce;

	void OnDestroy()
	{
		Instantiate (ExplosionForce, transform.position, Quaternion.identity);
	}

}
