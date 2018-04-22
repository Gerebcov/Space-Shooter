using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : BaseBullet {
	
	[SerializeField]
	float damage;
	[SerializeField]
	float energyLoss;

	void Update()
	{
		damage -= damage * energyLoss * TimeManager.LevelFrameTime;
	}

	public override void Contact (BaseGameObject Object)
	{
		Object.AddedDamage (damage, DamageTypes.Energy);
		Destroy (gameObject);
	}
}
