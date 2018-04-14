using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : BaseBullet {

	[SerializeField]
	GameObject ExplosionForce;

	public override void Contact (BaseGameObject Object)
	{
		ExplosionForce.transform.parent = null;
		ExplosionForce.SetActive (true);
		Destroy (gameObject);
	}

}
