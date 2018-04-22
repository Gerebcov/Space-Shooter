using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakingEngine : BaseEngine {

	public bool activ = true;

	public override void ActivateItem()
	{
		SetState ((int)EngineStates.Idle);
	}

	public override void DeactivateItem()
	{
		if(activ)
			SetState ((int)EngineStates.Activ);		
	}

	protected override void ActivUpdateHendler()
	{
		float currentForse = Mathf.Min (Forse, Rigidbody.velocity.magnitude * UnitMass);
		Rigidbody.AddForce( currentForse * -Rigidbody.velocity.normalized * TimeManager.LevelFrameTime, ForceMode2D.Impulse); 
	}

}
