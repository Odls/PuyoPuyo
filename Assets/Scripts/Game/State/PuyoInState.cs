using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoInState : StateBase{
	public PuyoInState(StateMachine p_stateMachine) : base(p_stateMachine) { }
	public override E_GAME_STATE stateEnum => E_GAME_STATE.PuyoIn;
	protected override bool enablePlayer => false;
	public override void Start() {
		base.Start();

		GameManager.instance.SetPlayerType(
			PuyoManager.GetRandomType(),
			PuyoManager.GetRandomType()
		);
		GameManager.instance.direction = E_MOVING_PUYO_DIRECTION.Up;
		GameManager.instance.SetPlayerPos(2, 12);

		stateMachine.SetState(E_GAME_STATE.Move);
	}
}