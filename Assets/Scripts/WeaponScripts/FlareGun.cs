using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGun : BaseWeapon {

	enum FlareGunStates
	{
		Idle,
		Fire
	}

	void Start()
	{
		InitializeStateManager (typeof(FlareGunStates));
		InitializeState ((int)FlareGunStates.Idle,
			null,
			null,
			null
		);
		InitializeState((int)FlareGunStates.Fire,
			null,
			FireUpdateHandler,
			null
		);
		SetState((int)FlareGunStates.Idle);
	}

	public override void ActivateItem ()
	{
		SetState ((int)FlareGunStates.Fire);
	}

	public override void DeactivateItem ()
	{
		SetState ((int)FlareGunStates.Idle);
	}

	void FireUpdateHandler()
	{
		if (Charged) {
			BulletSaple.SetStartVelosity (rigidbodyParent.velocity);
			Fire ();
		}
	}

}
