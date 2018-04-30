using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Module {

	[HideInInspector]
	public Unit OwnerUnit = null;
	[SerializeField]
	Transform attachmentAnchor = null;

	public UnitActionGroupTypes startActionGroup;

	[SerializeField]
	ModuleTypes type;
	public ModuleTypes Type{get { return type; } private set{type = value; }}
	[SerializeField]
	ModeleSizes size;
	public ModeleSizes Size{get { return size; } private set{size = value; }}

	[SerializeField]
	Item anchoredItem;
	public Item AnchoredItem{
		get { return anchoredItem; } 
		private set{
			anchoredItem = value; 
			Attachment ();
		}}


	public void AttachmentItem(Item item)
	{
		AnchoredItem = item;
		item.Establish (OwnerUnit);
	}

	public void Attachment()
	{
		AnchoredItem.transform.parent = attachmentAnchor;
		AnchoredItem.transform.localPosition = Vector3.zero;
		AnchoredItem.transform.localRotation = Quaternion.identity;
		AnchoredItem.transform.localScale = Vector3.one;
	}

	public void PullOffItem()
	{
		AnchoredItem.transform.parent = null;
		AnchoredItem.Dismantle ();
		AnchoredItem = null;
	}

}
