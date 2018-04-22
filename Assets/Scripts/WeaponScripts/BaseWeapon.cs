using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : Item {
	
	[SerializeField]
	protected float reloadingTime;
	protected float reloadingProgerss = 1;
	protected bool Charged{
		get{return reloadingProgerss == 1;}
	}
	[SerializeField]
	protected BaseBullet BulletSaple;
	[SerializeField]
	protected Rigidbody2D rigidbodyParent;
	[SerializeField]
	protected Transform spaunPoint = null;

	public override void Establish(Unit unit, Transform attachmentAnchor)
	{
		unit.Mass = mass;
		rigidbodyParent = unit.Rigidbodies[0];
		BulletSaple.SetFraction(unit.Fraction);
		Attachment (attachmentAnchor);
	}

	public virtual void Fire()
	{
		Instantiate (BulletSaple.gameObject, spaunPoint.position, spaunPoint.rotation);
		reloadingProgerss = 0;
		StartCoroutine (Reloading ());
	}

	IEnumerator Reloading()
	{
		while(true){
			reloadingProgerss = Mathf.Min(reloadingProgerss + (Time.deltaTime / reloadingTime), 1);
			if (Charged)
				break;
			yield return null;
		}
	}

}


