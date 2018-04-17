using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	[SerializeField]
	string unitName;
	public string UnitName{
		get{
			if (unitName == "")
				unitName = "Uniy №" + gameObject.GetInstanceID ();
			return unitName;
		}
	}

	public Rigidbody2D[] Rigidbodies;
	public float Mass{
		get{
			float mass = 0;
			foreach (Rigidbody2D R in Rigidbodies)
				mass += R.mass;
			return mass;
		}
	}

	public Module[] Modules;

	public List<ActionGroup> ActionGroups = new List<ActionGroup> ();

	public void AddedModuleInGroup(Module module, Constants.UnitActionGroupTypes groupType)
	{
		foreach (ActionGroup G in ActionGroups) 
		{
			if (G.GroupType == groupType) 
			{
				G.AddedModule (module);
				return;
			}
		}

		ActionGroups.Add (new ActionGroup (groupType, module));
	}

	public void RemoveModuleInGroup(Module module, Constants.UnitActionGroupTypes groupType)
	{
		foreach (ActionGroup G in ActionGroups) 
		{
			if (G.GroupType == groupType) 
			{
				G.RemoveModule (module);
				if(G.Modules.Count == 0)
					ActionGroups.Remove (G);
				return;
			}
		}
	}

	public void ActivateGroup(Constants.UnitActionGroupTypes Type)
	{
		foreach (ActionGroup G in ActionGroups) 
		{
			if (G.GroupType == Type) 
			{
				G.ActivateModules ();
				return;
			}
		}
	}

	public void DeactivateGroup(Constants.UnitActionGroupTypes Type)
	{
		foreach (ActionGroup G in ActionGroups) 
		{
			if (G.GroupType == Type) 
			{
				G.DeactivateModules ();
				return;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
