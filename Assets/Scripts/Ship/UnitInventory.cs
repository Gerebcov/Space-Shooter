using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInventory {

	[SerializeField]
	Unit Unit = null;

	[SerializeField]
	Transform ItemsParent = null;

	[SerializeField]
	float DropRange = 5f;

	public List<Item> Items = new List<Item>();

	public void InitializationInventory(Unit unit)
	{
		if (unit == null)
			return;
		Unit = unit;
		if (ItemsParent == null)
			ItemsParent = Unit.transform;
		foreach (Item I in Items) 
		{
			TakeItem (I);
		}
	}

	public void TakeItem(Item item)
	{
		item.Take ();
		Unit.Mass = item.Mass;
		item.transform.parent = ItemsParent;
		item.transform.localPosition = Vector3.zero;
	}

	public void DrotItem(Item item)
	{
		Unit.Mass = -item.Mass;
		item.Drop ();
		item.transform.parent = null;
		item.transform.position = Unit.transform.position + new Vector3 (Random.Range(1, 1 + DropRange) - (DropRange / 2), Random.Range(1, 1 + DropRange) - (DropRange / 2), 0);
	}

}
