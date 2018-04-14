using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : BaseBullet {

	public override void Contact (BaseGameObject Object)
	{
		Object.AddedDamage (Damage, Constants.DamageTypes.Energy);
		Destroy (gameObject);
	}
}
