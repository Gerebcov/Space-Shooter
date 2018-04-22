using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : StateManager {

	[SerializeField]
	protected float mass = 0;

	[SerializeField]
	ModuleTypes itemTypes;
	public ModuleTypes ItemTypes{get { return itemTypes; } private set{itemTypes = value; }}
	[SerializeField]
	ModeleSizes itemSize;
	public ModeleSizes ItemSize{get { return itemSize; } private set{itemSize = value; }}

	public virtual void Establish(Unit unit, Transform attachmentAnchor)
	{
		unit.Mass = mass;
		Attachment (attachmentAnchor);
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
