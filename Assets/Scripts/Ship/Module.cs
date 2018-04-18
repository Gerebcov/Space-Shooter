using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Module {

	[HideInInspector]
	public Unit OwnerUnit;
	[SerializeField]
	public Transform attachmentAnchor;
	public Transform AttachmentAnchor{get { return attachmentAnchor; } private set{attachmentAnchor = value; }}

	public UnitActionGroupTypes startActionGroup;

	[SerializeField]
	public ModuleTypes type;
	public ModuleTypes Type{get { return type; } private set{type = value; }}
	[SerializeField]
	public ModeleSizes size;
	public ModeleSizes Size{get { return size; } private set{size = value; }}

	[SerializeField]
	Item anchoredItem;
	public Item AnchoredItem{get { return anchoredItem; } private set{anchoredItem = value; }}


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
