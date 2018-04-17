using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGun : BaseWeapon {

	[SerializeField]
	Rocket rocket = null;
	[SerializeField]
	float startImpulse = 0;
	[SerializeField]
	Transform spaunPoint = null;

	enum FlareGunStates
	{
		Idle,
		Fire
	}

	void Start()
	{
		rocket = BulletSaple.GetComponent<Rocket> ();
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
			Fire ();
		}
	}

	public override void Fire ()
	{
		rocket.startImpulse = startImpulse;
		rocket.startVelosity = rigidbodyParent.velocity;
		Instantiate (BulletSaple, spaunPoint.position, spaunPoint.rotation);

		base.Fire ();
	}


}
