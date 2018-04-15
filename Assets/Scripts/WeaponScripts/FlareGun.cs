using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGun : BaseWeapon {

	[SerializeField]
	Rigidbody2D[] rocketRigibodys;
	[SerializeField]
	float startImpulse;
	[SerializeField]
	Transform spaunPoint;

	enum FlareGunStates
	{
		Idle,
		Fire
	}

	void Start()
	{
		rocketRigibodys = BulletSaple.GetComponentsInChildren<Rigidbody2D> ();
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

	public override void FireStart ()
	{
		SetState ((int)FlareGunStates.Fire);
	}

	public override void FireEnd ()
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
		foreach (Rigidbody2D R in rocketRigibodys) {
			R.velocity = rigidbodyParent.velocity;
			R.AddForce (startImpulse * R.transform.up, ForceMode2D.Impulse);
		}
		Instantiate (BulletSaple, spaunPoint.position, spaunPoint.rotation);
		base.Fire ();
	}


}
