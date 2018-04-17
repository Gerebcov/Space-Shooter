using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Module  {

	public Unit OwnerUnit;
	public Transform AttachmentAnchor;

	public Constants.ModuleTypes Type;
	public Constants.ModeleSizes Size;

	public Item AnchoredItem;


	public void AttachmentItem(Item item)
	{
		AnchoredItem = item;
		item.Establish (this);
	}

	public void PullOffItem()
	{
		AnchoredItem.PullOff ();
		AnchoredItem = null;
	}

}
