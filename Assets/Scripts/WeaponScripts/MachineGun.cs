using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : BaseWeapon {

	CommonBullet bullet;
	[SerializeField]
	float maxAngelDeviation;
	[SerializeField]
	Transform spawnPoint;

	enum MachineGunStates
	{
		Idle,
		Fire
	}

	void Start () {
		
		bullet = BulletSaple.GetComponent <CommonBullet> ();
		InitializeStateManager (typeof(MachineGunStates));
		InitializeState (
			(int)MachineGunStates.Idle,
			null,
			null,
			null
		);
		InitializeState (
			(int)MachineGunStates.Fire,
			null,
			FireUpdateHandler,
			null
		);
	}
	public override void ActivateItem ()
	{
		SetState ((int)MachineGunStates.Fire);
	}
	public override void DeactivateItem ()
	{
		SetState ((int)MachineGunStates.Idle);
	}

	void FireUpdateHandler ()
	{
		if (!Charged)
			return;
		bullet.StartVelocity = rigidbodyParent.velocity;
		Instantiate (BulletSaple, spawnPoint.position, spawnPoint.rotation);
		Fire ();
	}



}
