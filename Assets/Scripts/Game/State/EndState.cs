using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : StateBase {
	public EndState(StateMachine p_stateMachine) : base(p_stateMachine) { }

	public override E_GAME_STATE stateEnum => E_GAME_STATE.End;

	public override void Start() {
		base.Start();
		Debug.Log("Game Over");
	}
}
