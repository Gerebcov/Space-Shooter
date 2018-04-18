using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionGroup {

	public UnitActionGroupTypes GroupType;
	List<Module> modules = new List<Module> ();
	public List<Module> Modules{get { return modules; }}

	public ActionGroup(UnitActionGroupTypes type, Module module)
	{
		Modules.Add (module);
		GroupType = type;
	}

	public void AddedModule(Module module)
	{
		Modules.Add (module);
	}

	public void RemoveModule(Module module)
	{
		Modules.Remove (module);
	}

	public void ActivateModules()
	{
		foreach (Module Module in Modules) {
			Module.AnchoredItem.ActivateItem ();
		}
	}

	public void DeactivateModules()
	{
		foreach (Module Module in Modules) {
			Module.AnchoredItem.DeactivateItem ();
		}
	}
}
