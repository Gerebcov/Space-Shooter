using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIButton : UIElement {

	[SerializeField]
	float EnterSize = 0.9f, PressedSize = 0.75f, ScaleSpeed = 0.5f;

	public UnityEvent A = new UnityEvent();

	public override void EnterUpdateHandler()
	{
		if (Scaling (EnterSize))
			SetState ((int)UIElement.ElamentStates.Idle);
	}

	public override void ExitUpdateHandler()
	{
		if (Scaling (1f))
			SetState ((int)UIElement.ElamentStates.Idle);
	}

	public override void PressedUpdateHandler()
	{
		if (Scaling (PressedSize))
			SetState ((int)UIElement.ElamentStates.Idle);
	}

	public override void DepressedUpdateHandler()
	{
		if (Scaling (EnterSize))
			SetState ((int)UIElement.ElamentStates.Idle);
	}

	public bool Scaling(float targetScale)
	{
		if (transform.localScale.x == targetScale)
			return true;

		float scale = transform.localScale.x;
		if (scale > targetScale)
			scale = Mathf.Max (scale - (ScaleSpeed * Time.unscaledDeltaTime), targetScale);
		else if(scale < targetScale)
			scale = Mathf.Min (scale + (ScaleSpeed * Time.unscaledDeltaTime), targetScale);

		transform.localScale = new Vector3 (scale, scale, 1);
		return false;
	}

}
