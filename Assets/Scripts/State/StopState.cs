using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Stop;
	public override void Start() {
		base.Start();
		BoardManager.instance.player.gameObject.SetActive(false);
		GameManager.instance.ApplyPlayer();
		StateManager.instance.SetState(E_GAME_STATE.Drop);
	}
}
