using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateMethod();

public class StateManager : MonoBehaviour {

	protected State[] stages = null;
	protected State currentState = null;

	protected void InitializeStages(int StageLength, State[] Stages)
	{
		stages = new State[StageLength];
		foreach (State S in Stages) {
			stages [S.StateID] = S;
		}
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

	public State (int ID, StateMethod Start, StateMethod Update, StateMethod End)
	{
		StateID = ID;
		StartState = Start;
		UpdateState = Update;
		EndState = End;
	}
}


