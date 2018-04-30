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
		StartCoroutine (StateUpdate ());
	}

	protected void InitializeState (int ID, StateMethod StartState, StateMethod UpdateState, StateMethod EndState)
	{
		stages [ID] = new State(ID, StartState, UpdateState, EndState);

	}

	protected void SetState(int StateID)
	{
		if (currentState != null && currentState.EndState != null)
			currentState.EndState ();
		currentState = stages [StateID];
		if (currentState != null && currentState.StartState != null)
			currentState.StartState ();
	}

	IEnumerator StateUpdate()
	{
		while (true) {
			if (currentState != null && currentState.UpdateState != null) {
				currentState.UpdateState ();
			}
			yield return null;
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

	public State (int stateID, StateMethod Start, StateMethod Update, StateMethod End)
	{
		StateID = stateID;
		StartState = Start;
		UpdateState = Update;
		EndState = End;
	}
}


