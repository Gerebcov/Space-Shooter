using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : StateManager {

	public enum ElamentStates
	{
		Idle,
		Enter,
		Pressed,
		Depressed,
		Exit
	}

	public static UIElement isPressed = null;
	protected bool isEnter = false;
	public bool IsEnter {get{return isEnter;}}

	public virtual void OnClick()
	{

	}

	public virtual void OnEnter()
	{
		isEnter = true;
		SetState ((int)ElamentStates.Enter);
	}

	public virtual void OnExit()
	{
		isEnter = false;
		SetState ((int)ElamentStates.Exit);
	}

	public virtual void OnPressed()
	{
		isPressed = this;
		SetState ((int)ElamentStates.Pressed);
	}

	public virtual void OnDepressed()
	{
		if (isPressed == this) {
			OnClick ();
		}
		isPressed = null;
		SetState ((int)ElamentStates.Depressed);
	}

	void Awake()
	{
		InitializeStateManager (typeof(ElamentStates));
		InitializeState (
			(int)ElamentStates.Idle,
			null,
			null,
			null);
		InitializeState (
			(int)ElamentStates.Enter,
			null,
			EnterUpdateHandler,
			null);
		InitializeState (
			(int)ElamentStates.Pressed,
			null,
			PressedUpdateHandler,
			null);
		InitializeState (
			(int)ElamentStates.Depressed,
			null,
			DepressedUpdateHandler,
			null);
		InitializeState (
			(int)ElamentStates.Exit,
			null,
			ExitUpdateHandler,
			null);
		SetState ((int)ElamentStates.Idle);
	}

	public virtual void EnterStartHandler()
	{

	}

	public virtual void ExitStartHandler()
	{

	}

	public virtual void PressedStartHandler()
	{

	}

	public virtual void DepressedStartHandler()
	{

	}

	public virtual void EnterUpdateHandler()
	{

	}

	public virtual void ExitUpdateHandler()
	{

	}

	public virtual void PressedUpdateHandler()
	{

	}

	public virtual void DepressedUpdateHandler()
	{

	}

	public virtual void EnterEndHandler()
	{

	}

	public virtual void ExitEndHandler()
	{

	}

	public virtual void PressedEndHandler()
	{

	}

	public virtual void DepressedEndHandler()
	{

	}


}
