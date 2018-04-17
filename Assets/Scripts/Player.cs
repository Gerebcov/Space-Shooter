using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	Unit PlayerSchip;

	[SerializeField]
	KeyGroup[] ActionKeyGroup;

	// Use this for initialization
	void Awake () {
		foreach (Module M in PlayerSchip.Modules) 
		{
			M.AttachmentItem (M.AnchoredItem);
			if(Constants.UnitActionGroupTypes.Weapon.ToString() == M.AnchoredItem.ItemTypes.ToString())
				PlayerSchip.AddedModuleInGroup (M, Constants.UnitActionGroupTypes.Weapon);
			if(Constants.UnitActionGroupTypes.Engine.ToString() == M.AnchoredItem.ItemTypes.ToString())
				PlayerSchip.AddedModuleInGroup (M, Constants.UnitActionGroupTypes.Engine);
		}
	}
	
	// Update is called once per frame
	void Update () {

		foreach (KeyGroup K in ActionKeyGroup) 
		{
			if(Input.GetKeyDown(K.GroupKey))
				PlayerSchip.ActivateGroup (K.GroupType);

			if(Input.GetKeyUp(K.GroupKey))
				PlayerSchip.DeactivateGroup (K.GroupType);
		}

		Vector3 mousePosition = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
		float rotation_z = Mathf.Atan2(-mousePosition.normalized.x, mousePosition.normalized.y) * Mathf.Rad2Deg;
		PlayerSchip.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);    

		Camera.main.transform.position = PlayerSchip.transform.position + (Vector3.forward * Camera.main.transform.position.z);


	}
}
[System.Serializable]
public class KeyGroup{

	public KeyCode GroupKey;
	public Constants.UnitActionGroupTypes GroupType;

}
