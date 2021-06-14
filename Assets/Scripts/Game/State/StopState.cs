using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : StateBase {
	public StopState(StateMachine p_stateMachine) : base(p_stateMachine) { }
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Stop;
	public override void Start() {
		base.Start();
		GameManager.instance.ApplyPlayer();
		stateMachine.SetState(E_GAME_STATE.Drop);
	}
}
