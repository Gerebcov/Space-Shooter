using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : Item {

	protected static GameObject bulletParent; 

	enum WeaponStates
	{
		Idle,
		Fire,
		Reloading
	}

	
	[SerializeField]
	protected float reloadingTime = 0.5f;
	protected float reloadingProgerss = 1;
	protected bool Charged{
		get{return reloadingProgerss == 1;}
	}
	[SerializeField]
	int collarSize = 1;
	int lastBullet = 1;
	[SerializeField]
	float fireRate = 0.2f;
	float lastFireTime = 0;
	[SerializeField]
	float deflectionAngle = 0;
	[SerializeField]

	protected BaseBullet BulletSaple =null;
	[SerializeField]
	bool useRelativeVelosity = true;
	public bool UseRelativeVelosity{get{return useRelativeVelosity;}}
	[SerializeField]
	float bulletAcceleration = 50;
	public float BulletAcceleration{get {return bulletAcceleration;}}

	[SerializeField]
	protected Rigidbody2D rigidbodyParent = null;
	[SerializeField]
	protected Transform spaunPoint = null;
	public Transform SpaunPoint{get{return spaunPoint;}}

	void Start()
	{
		InitializeStateManager (typeof(WeaponStates));
		InitializeState ((int)WeaponStates.Idle,
			null,
			null,
			null
		);
		InitializeState((int)WeaponStates.Fire,
			FireStartHandler,
			FireUpdateHandler,
			FireEndHandler
		);
		InitializeState((int)WeaponStates.Reloading,
			ReloadingStartHandler,
			ReloadingUpdateHandler,
			ReloadingEndHandler
		);
		SetState((int)WeaponStates.Idle);
		if (!bulletParent)
			bulletParent = new GameObject ("Bullets");
	}

	public override void ActivateItem ()
	{
		SetState ((int)WeaponStates.Reloading);
		base.ActivateItem ();
	}

	public override void DeactivateItem ()
	{
		SetState ((int)WeaponStates.Reloading);
		base.DeactivateItem ();
	}

	public virtual void FireStartHandler()
	{
		lastBullet = collarSize;
	}

	public virtual void FireUpdateHandler()
	{
		if (lastBullet > 0) {
			if (Time.time >= lastFireTime + fireRate) {
				Fire ();
				lastFireTime = Time.time;
				lastBullet--;
			}
		} else {
			SetState ((int)WeaponStates.Reloading);
		}
	}

	public virtual void FireEndHandler()
	{
		if(lastBullet != collarSize)
			reloadingProgerss = 0;
	}

	public virtual void ReloadingStartHandler()
	{

	}

	public virtual void ReloadingUpdateHandler()
	{
		reloadingProgerss = Mathf.Min(reloadingProgerss + (Time.deltaTime / reloadingTime), 1);
		if (Charged) {
			if (isActiv)
				SetState ((int)WeaponStates.Fire);
			else
				SetState ((int)WeaponStates.Idle);
		}
			
	}

	public virtual void ReloadingEndHandler()
	{

	}


	public override void Establish(Unit unit)
	{
		rigidbodyParent = unit.Rigidbodies[0];
		BulletSaple.SetFraction(unit.Fraction, unit.ID);
		base.Establish (unit);
	}

	public virtual void Fire()
	{
		if (deflectionAngle != 0)
			spaunPoint.localEulerAngles = new Vector3 (0, 0, Random.Range (-deflectionAngle, deflectionAngle));
		BulletSaple.SetStartVelosity (bulletAcceleration, useRelativeVelosity? rigidbodyParent.velocity: Vector2.zero);
		Instantiate (BulletSaple.gameObject, spaunPoint.position, spaunPoint.rotation, bulletParent.transform);
	}

}


