using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : StateManager {
	
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

	public void Establish(ref System.Action ActionStart, ref System.Action ActionStop, Rigidbody2D Rigidbody, Transform AttachmentAnchor)
	{
		ActionStart += FireStart;
		ActionStop += FireEnd;
		rigidbodyParent = Rigidbody;
		Attachment (AttachmentAnchor);
	}

	public void Attachment(Transform AttachmentAnchor)
	{
		transform.parent = AttachmentAnchor;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}


	public virtual void FireStart ()
	{
		
	}

	public virtual void FireEnd ()
	{
		
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


