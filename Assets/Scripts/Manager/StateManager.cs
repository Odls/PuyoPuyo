using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_GAME_STATE {
	None = -1,
	PuyoIn,
	Move,
	Stop,
	Drop,
	Remove,
	End
}

public class StateManager : ManagerBase<StateManager> {
	StateBase nowState;
	public E_GAME_STATE nowStateEnum => (nowState == null) ? E_GAME_STATE.None : nowState.stateEnum;

	Dictionary<E_GAME_STATE, StateBase> stateDict = new Dictionary<E_GAME_STATE, StateBase>();

	public void AddState(E_GAME_STATE p_stateEnum, StateBase p_state) {
		if (stateDict.ContainsKey(p_stateEnum)) {
			Debug.LogError("Already Has State : " + p_state);
		} else {
			stateDict.Add(p_stateEnum, p_state);
		}
	}

	public void SetState(E_GAME_STATE p_state) {
		StateBase _state;
		if (stateDict.TryGetValue(p_state, out _state)) {
			if (nowState != null) {
				nowState.End();
			}
			nowState = _state;

			nowState.Start();

		} else {
			Debug.LogError("No State : " + p_state);
		}
	}

	private void Update() {
		if (nowState != null) {
			nowState.Update();
		}
	}
}
