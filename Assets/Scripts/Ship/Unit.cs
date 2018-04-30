using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : BaseGameObject {


	public override string UnitName{
		get{
			if (unitName == "")
				unitName = "Uniy №" + gameObject.GetInstanceID ();
			return unitName;
		}
	}

	[SerializeField]
	Module[] modules = null;
	public Module[] Modules {get {return modules;}}

	List<ActionGroup> ActionGroups = new List<ActionGroup> ();

	[SerializeField]
	UnitInventory Inventory = new UnitInventory();

	void Start()
	{
		Inicialization ();
		Inventory.InitializationInventory (this);
		foreach (Module M in Modules) {
			M.OwnerUnit = this;
			if (M.AnchoredItem != null) {
				M.AttachmentItem (M.AnchoredItem);
				AddedModuleInGroup (M, M.startActionGroup);
			}
		}
	}

	public void AddedModuleInGroup(Module module, UnitActionGroupTypes groupType)
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

	public void RemoveModuleInGroup(Module module, UnitActionGroupTypes groupType)
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

	public void ActivateGroup(UnitActionGroupTypes Type)
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

	public void DeactivateGroup(UnitActionGroupTypes Type)
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

}
