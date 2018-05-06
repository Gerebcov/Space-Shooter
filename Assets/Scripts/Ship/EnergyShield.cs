using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : Item {

	[SerializeField]
	Shield shildObject = null;

	List<Collider2D> baseCollisrs = new List<Collider2D>();

	[SerializeField]
	float recoveryPerSecond = 0;

	Unit objectInShield; 

	enum EnergyShieldStates
	{
		Idle,
		Activate,
		Recharge
	}

	void Start()
	{
		InitializeStateManager (typeof(EnergyShieldStates));
		InitializeState (
			(int)EnergyShieldStates.Idle,
			null,
			IdleUpdateHandler,
			null
		);
		InitializeState (
			(int)EnergyShieldStates.Activate,
			ActivateStartHandler,
			ActivateUpdateHandler,
			ActivateEndHandler
		);
		InitializeState (
			(int)EnergyShieldStates.Recharge,
			RechargeStartHandler,
			RechargeUpdateHandler,
			RechargeEndHandler
		);
		SetState ((int)EnergyShieldStates.Idle);
	}

	public override void Establish(Unit unit)
	{
		foreach (Rigidbody2D R in unit.Rigidbodies) 
		{
			foreach (Collider2D C in R.GetComponents<Collider2D>()) 
			{
				baseCollisrs.Add (C);
			}
		}
		objectInShield = unit;
		shildObject.SetParentUnit (unit);
		base.Establish (unit);
	}

	public override void ActivateItem()
	{
		switch (currentState.StateID) 
		{
		case (int)EnergyShieldStates.Idle:
			{
				SetState ((int)EnergyShieldStates.Activate);
				break;
			}
		case (int)EnergyShieldStates.Activate:
			{
				SetState ((int)EnergyShieldStates.Idle);
				break;
			}
		}

	}

	void IdleUpdateHandler()
	{
		if (shildObject.HealsPoint < shildObject.MaxHealsPoint)
			shildObject.Recharge (recoveryPerSecond * TimeManager.LevelFrameTime);
	}

	void ActivateStartHandler()
	{
		SetActivBaseCollider (false);
		objectInShield.IsImortal = true;
		shildObject.gameObject.SetActive (true);
		shildObject.IsImortal = false;
	}

	void ActivateUpdateHandler()
	{
		if(shildObject.gameObject.activeSelf == false){
			SetState ((int)EnergyShieldStates.Recharge);
			return;
		}
		if (shildObject.HealsPoint < shildObject.MaxHealsPoint)
			shildObject.Recharge (recoveryPerSecond * 0.25f * TimeManager.LevelFrameTime);
	}

	void ActivateEndHandler()
	{
		shildObject.gameObject.SetActive (false);
		objectInShield.IsImortal = false;
		SetActivBaseCollider (true);
		shildObject.IsImortal = true;
	}

	void RechargeStartHandler()
	{

	}

	void RechargeUpdateHandler()
	{
		shildObject.Recharge (recoveryPerSecond * 0.5f * TimeManager.LevelFrameTime);
		if (shildObject.HealsPoint == shildObject.MaxHealsPoint)
			SetState ((int)EnergyShieldStates.Idle);
	}

	void RechargeEndHandler()
	{

	}

	void SetActivBaseCollider(bool Activ)
	{
		foreach (Collider2D C in baseCollisrs)
			C.enabled = Activ;
	}

}
