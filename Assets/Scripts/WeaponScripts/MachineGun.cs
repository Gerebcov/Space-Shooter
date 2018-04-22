using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : BaseWeapon {


	[SerializeField]
	float maxAngelDeviation;

	enum MachineGunStates
	{
		Idle,
		Fire
	}

	void Start () {
		
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
		BulletSaple.SetStartVelosity(rigidbodyParent.velocity);
		Fire ();
	}



}
