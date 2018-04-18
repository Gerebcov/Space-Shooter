﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEngine : Item {

	public enum EngineStates
	{
		Idle,
		Activ
	}
	
	[SerializeField]
	Rigidbody2D Rigidbody;
	[SerializeField]
	float Forse;
	float UnitMass;
	[SerializeField]
	float sqrMaxSpeed;

	[SerializeField]
	bool AutoStart = false;

	[SerializeField]
	ParticleSystem particle = null;

	public void CalculateMaxSpeed()
	{
		sqrMaxSpeed = Mathf.Pow(Forse / UnitMass, 2);
	}

	// Use this for initialization
	void Start () {
		InitializeStateManager (typeof(EngineStates));
		InitializeState (
			(int)EngineStates.Idle,
			null,
			null,
			null);
		InitializeState (
			(int)EngineStates.Activ,
			ActivStartHendler,
			ActivUpdateHendler,
			ActivEndHendler);
		if (AutoStart) {
			CalculateMaxSpeed ();
			SetState ((int)EngineStates.Activ);
		}
	}
	
	public override void Establish(Module module)
	{
		Rigidbody = module.OwnerUnit.Rigidbodies[0];
		Attachment (module.AttachmentAnchor);
		UnitMass = module.OwnerUnit.Mass;
		CalculateMaxSpeed ();
	}

	public override void ActivateItem()
	{
		SetState ((int)EngineStates.Activ);
	}

	public override void DeactivateItem()
	{
		SetState ((int)EngineStates.Idle);
	}

	void ActivStartHendler()
	{
		if (particle)
			particle.Play (true);
	}

	void ActivUpdateHendler()
	{
		Rigidbody.AddForce((Vector2)transform.up * Forse * (1 - ((new Vector2(Mathf.Max(Rigidbody.velocity.x * transform.up.x, 0), Mathf.Max(Rigidbody.velocity.y * transform.up.y, 0)).sqrMagnitude / sqrMaxSpeed))) * TimeManager.LevelFrameTime, ForceMode2D.Impulse); 
	}

	void ActivEndHendler()
	{
		if (particle)
			particle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
	}


}
