using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : BaseBullet {

	[SerializeField]
	GameObject ExplosionObject = null;

	void OnDestroy()
	{
		Instantiate (ExplosionObject, transform.position, Quaternion.identity);
	}

}
