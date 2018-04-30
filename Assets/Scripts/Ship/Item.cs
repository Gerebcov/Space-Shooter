using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : StateManager {

	[SerializeField]
	private float mass = 0;
	public float Mass{get { return mass;}}

	protected bool isActiv = false;

	[SerializeField]
	ModuleTypes itemTypes;
	public ModuleTypes ItemTypes{get { return itemTypes; } private set{itemTypes = value; }}
	[SerializeField]
	ModeleSizes itemSize;
	public ModeleSizes ItemSize{get { return itemSize; } private set{itemSize = value; }}

	[SerializeField]
	GameObject visualizeItem, visualizeDrop;
	[SerializeField]
	Texture visualizeImage;
	public Texture VisualizeImage {get{return visualizeImage; }}

	public virtual void Take()
	{
		if(visualizeDrop)
			visualizeDrop.SetActive (false);
	}

	public virtual void Establish(Unit unit)
	{
		if(visualizeItem)
			visualizeItem.SetActive (true);
	}

	public virtual void Dismantle()
	{
		if(visualizeItem)
			visualizeItem.SetActive (false);
	}

	public void Drop()
	{
		if(visualizeDrop)
			visualizeDrop.SetActive (true);
	}

	public virtual void ActivateItem()
	{
		isActiv = true;
	}

	public virtual void DeactivateItem()
	{
		isActiv = false;
	}
}
