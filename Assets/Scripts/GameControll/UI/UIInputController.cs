using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour {

	[SerializeField]
	Camera uiCamera;
	public static Camera UICamera{get {return UICamera; }}

	[SerializeField]
	float uiZone;

	public static bool Activ = true;

	static UIElement targetElament;
	public static UIElement TargetElament
	{
		get{return targetElament; }
		private set {
			if (targetElament != value) {
				if (targetElament)
					targetElament.OnExit ();
				targetElament = value;
				if (targetElament) {
					targetElament.OnEnter ();
				}
			}
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0) && TargetElament != null) {
			TargetElament.OnPressed ();
		}
		if (Input.GetMouseButtonUp (0)) {
			if (TargetElament != null)
				TargetElament.OnDepressed ();
			else
				UIElement.isPressed = null;
		}

		if (!Activ)
			return;
		RaycastHit hit;
		Ray ray = uiCamera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, uiZone))
			TargetElament = hit.collider.gameObject.GetComponent<UIElement> ();
		else
			TargetElament = null;


			
	}

}
