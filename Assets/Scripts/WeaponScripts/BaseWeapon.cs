using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : Item {
	
	[SerializeField]
	protected float reloadingTime;
	protected float reloadingProgerss = 1;
	public bool Charged{
		get{return reloadingProgerss == 1;}
	}
	[SerializeField]
	protected GameObject BulletSaple;
	[SerializeField]
	protected Rigidbody2D rigidbodyParent;

	public override void Establish(Module module)
	{
		rigidbodyParent = module.OwnerUnit.Rigidbodies[0];
		Attachment (module.AttachmentAnchor);
	}

	public virtual void Fire()
	{
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


