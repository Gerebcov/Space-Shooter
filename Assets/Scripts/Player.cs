using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[SerializeField]
	Unit PlayerShip;

	[SerializeField]
	KeyGroup[] ActionKeyGroup;


	// Update is called once per frame
	void Update () {

		if (PlayerShip == null) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			return;
		}

		foreach (KeyGroup K in ActionKeyGroup) 
		{
			if(Input.GetKeyDown(K.GroupKey))
				PlayerShip.ActivateGroup (K.GroupType);

			if(Input.GetKeyUp(K.GroupKey))
				PlayerShip.DeactivateGroup (K.GroupType);
		}

		Vector3 mousePosition = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
		float rotation_z = Mathf.Atan2(-mousePosition.normalized.x, mousePosition.normalized.y) * Mathf.Rad2Deg;
		PlayerShip.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);    

		Camera.main.transform.position = PlayerShip.transform.position + (Vector3.forward * Camera.main.transform.position.z);


	}
}
[System.Serializable]
public class KeyGroup{

	public KeyCode GroupKey;
	public UnitActionGroupTypes GroupType;

}
