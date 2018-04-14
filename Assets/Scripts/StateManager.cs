using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateMethod();

public class StateManager : MonoBehaviour {

	protected State[] stages = null;
	protected State currentState = null;

	protected void InitializeStateManager(System.Type Enum)
	{
		stages = new State[System.Enum.GetNames (Enum).Length];
	}

	protected void InitializeState (int ID, StateMethod StartState, StateMethod UpdateState, StateMethod EndState)
	{
		stages [ID] = new State(StartState, UpdateState, EndState);
	}

	protected void SetState(int StateID)
	{
		StopCoroutine (StateUpdate ());
		if (currentState != null && currentState.EndState != null)
			currentState.EndState ();
		currentState = stages [StateID];
		if (currentState != null && currentState.StartState != null)
			currentState.StartState ();
		StartCoroutine (StateUpdate ());
	}

	private IEnumerator StateUpdate()
	{
		if (currentState != null && currentState.UpdateState != null) {
			while (true) {
				currentState.UpdateState ();
				yield return null;
			}
		}
	}
}


[System.Serializable]
public class State
{
	public int StateID;

	public StateMethod StartState;
	public StateMethod UpdateState;
	public StateMethod EndState;

	public State (StateMethod Start, StateMethod Update, StateMethod End)
	{
		StartState = Start;
		UpdateState = Update;
		EndState = End;
	}
}


