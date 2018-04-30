using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEngine : Item {
	
	public enum EngineStates
	{
		Idle,
		Activ
	}
	
	[SerializeField]
	protected Rigidbody2D Rigidbody;
	[SerializeField]
	protected float Forse = 0;
	protected float UnitMass;
	[SerializeField]
	float sqrMaxSpeed;

	[SerializeField]
	bool AutoStart = false;

	[SerializeField]
	protected ParticleSystem particle = null;

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
	
	public override void Establish(Unit unit)
	{
		Rigidbody = unit.Rigidbodies[0];
		UnitMass = unit.Mass;
		CalculateMaxSpeed ();
		base.Establish (unit);
	}

	public override void ActivateItem()
	{
		SetState ((int)EngineStates.Activ);
	}

	public override void DeactivateItem()
	{
		SetState ((int)EngineStates.Idle);
	}

	protected virtual void ActivStartHendler()
	{
		if (particle)
			particle.Play (true);
	}

	protected virtual void ActivUpdateHendler()
	{
		Rigidbody.AddForce((Vector2)transform.up * Forse * (1 - ((new Vector2(Mathf.Max(Rigidbody.velocity.x * transform.up.x, 0), Mathf.Max(Rigidbody.velocity.y * transform.up.y, 0)).sqrMagnitude / sqrMaxSpeed))) * TimeManager.LevelFrameTime, ForceMode2D.Impulse); 
	}

	protected virtual void ActivEndHendler()
	{
		if (particle)
			particle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
	}


}
