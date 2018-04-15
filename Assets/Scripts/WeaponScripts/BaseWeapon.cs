using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {
	
	protected bool fireOnOff = false;

	[SerializeField]
	protected float [] SpeedFire;
	protected float lastFireTime;
	[SerializeField]
	protected float[] damage;
	protected Rigidbody2D player;


	public virtual void FireStart ()
	{
		fireOnOff = true;
		StartCoroutine (FireUpdate());
	}

	protected virtual IEnumerator FireUpdate ()
	{
		yield return null;
	}

	public virtual void FireEnd ()
	{
		StopCoroutine (FireUpdate());
		fireOnOff = false;
	}

}


