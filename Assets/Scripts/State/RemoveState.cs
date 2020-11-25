using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Remove;

	public override void Start() {
		base.Start();
		BoardManager.instance.RemoveDiePuyo();
		BoardManager.instance.ShowRealCells();

		List<LinkInfo> _linkInfos = BoardManager.instance.DoRemoveLink();
		BoardManager.instance.ShowRealCells();

		if(_linkInfos.Count > 0) {
			StateManager.instance.SetState(E_GAME_STATE.Drop);
		} else {
			StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
		}
	}
}