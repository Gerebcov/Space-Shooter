using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UICameraZone : MonoBehaviour {

	[SerializeField]
	Camera uiCamera;
	
	// Update is called once per frame
	void Update () {
		if (!uiCamera)
			return;
		float screenProportion = (float)uiCamera.pixelWidth / (float)uiCamera.pixelHeight;
		Vector3 globalSize = new Vector3 (uiCamera.orthographicSize * screenProportion, uiCamera.orthographicSize, uiCamera.farClipPlane);

		Debug.DrawRay (new Vector3(globalSize.x, globalSize.y, -globalSize.z) + uiCamera.transform.position,
			Vector3.down * globalSize.y * 2, Color.red);
		Debug.DrawRay (new Vector3(globalSize.x, globalSize.y, -globalSize.z) + uiCamera.transform.position,
			Vector3.left * globalSize.x * 2, Color.red);
		Debug.DrawRay (new Vector3(-globalSize.x, -globalSize.y, -globalSize.z) + uiCamera.transform.position,
			Vector3.up * globalSize.y * 2, Color.red);
		Debug.DrawRay (new Vector3(-globalSize.x, -globalSize.y, -globalSize.z) + uiCamera.transform.position,
			Vector3.right * globalSize.x * 2, Color.red);
	}
}
