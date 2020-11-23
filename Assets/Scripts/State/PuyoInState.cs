using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoInState : StateBase
{
    public override E_GAME_STATE stateEnum => E_GAME_STATE.PuyoIn;

	public override void Start() {
		base.Start();

		BoardManager.instance.player.gameObject.SetActive(true);

		GameManager.instance.SetType(
			PuyoManager.instance.GetRandomType(),
			PuyoManager.instance.GetRandomType()
		);
		GameManager.instance.direction = E_MOVING_PUYO_DIRECTION.Up;
		GameManager.instance.SetPos(2, 12);

		StateManager.instance.SetState(E_GAME_STATE.Move);
	}
}