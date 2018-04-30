using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[SerializeField]
	Unit PlayerShip = null;

	[SerializeField]
	KeyGroup[] ActionKeyGroup = null;
	[SerializeField]
	UIElement gameplayZona = null;

	[SerializeField]
	TextMesh healsText = null;
	[SerializeField]
	SpriteRenderer healsBack = null;

	[SerializeField]
	float cameraOffset;

	// Update is called once per frame
	void Update () {

		if (PlayerShip == null) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			return;
		}
		Vector3 mousePosition = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
		Vector2 offset = (mousePosition.sqrMagnitude / new Vector3 (Screen.width / 2, Screen.height / 2, 0).sqrMagnitude) * cameraOffset * mousePosition.normalized;
		Camera.main.transform.position = PlayerShip.transform.position + (Vector3)offset + (Vector3.forward * Camera.main.transform.position.z);

		healsText.text = ((int)PlayerShip.MaxHealsPoint).ToString () + "/" + ((int)PlayerShip.HealsPoint).ToString ();
		float healsProportion =  PlayerShip.HealsPoint / PlayerShip.MaxHealsPoint;
		Color color;
		if (healsProportion > 0.5f)
			color = Color.gray;
		else if (healsProportion > 0.25f)
			color = Color.yellow;
		else
			color = Color.red;

		healsBack.color = color;

		if (!gameplayZona.IsEnter)
			return;
		if (UIElement.isPressed == gameplayZona)
			UIInputController.Activ = false;
		else
			UIInputController.Activ = true;

		foreach (KeyGroup K in ActionKeyGroup) 
		{
			if(Input.GetKeyDown(K.GroupKey))
				PlayerShip.ActivateGroup (K.GroupType);

			if(Input.GetKeyUp(K.GroupKey))
				PlayerShip.DeactivateGroup (K.GroupType);
		}


		float rotation_z = Mathf.Atan2(-mousePosition.normalized.x, mousePosition.normalized.y) * Mathf.Rad2Deg;
		PlayerShip.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);    

	}
}
[System.Serializable]
public class KeyGroup{

	public KeyCode GroupKey;
	public UnitActionGroupTypes GroupType;

}
