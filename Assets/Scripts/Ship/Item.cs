using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : StateManager {

	[SerializeField]
	float mass;

	[SerializeField]
	public Constants.ModuleTypes ItemTypes;

	public virtual void Establish( Module module)
	{

	}

	public virtual void PullOff()
	{

	}

	public virtual void Attachment(Transform AttachmentAnchor)
	{
		transform.parent = AttachmentAnchor;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	public virtual void ActivateItem()
	{
		
	}

	public virtual void DeactivateItem()
	{

	}
}
