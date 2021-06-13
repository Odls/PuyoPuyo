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
public abstract class StateBase {
	readonly protected StateMachine stateMachine;
	public StateBase(StateMachine p_stateMachine) {
		stateMachine = p_stateMachine;
	}

	protected virtual bool enablePlayer => false;

	public abstract E_GAME_STATE stateEnum { get; }

	public virtual void Start() {
		GameManager.instance.enablePlayer = enablePlayer;
	}
	public virtual void Update() {}
	public virtual void End() {}
}
