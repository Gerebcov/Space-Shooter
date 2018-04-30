using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : BaseBullet {
	
	[SerializeField]
	float damage = 100f;
	[SerializeField]
	float energyLoss = 0.1f;

	void Update()
	{
		damage -= damage * energyLoss * TimeManager.LevelFrameTime;
	}

	public override void Contact (BaseGameObject Object)
	{
		if (Object.Fraction != fraction)
			Object.AddedDamage (damage, DamageTypes.Energy);
		Destroy (gameObject);
	}
}
