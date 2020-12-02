using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Move;

	public override void Start() {
		base.Start();
		autoDownCoroutine = StateManager.instance.StartCoroutine(IeAutoDown());
	}
	WaitForSeconds waitForSeconds = null;
	Coroutine autoDownCoroutine = null;
	IEnumerator IeAutoDown() {
		if(waitForSeconds == null) {
			waitForSeconds = new WaitForSeconds(BoardManager.instance.playerDownDelay);
		}

		while (true) {
			yield return waitForSeconds;
			if(!GameManager.instance.PlayerMove(0, -1)) {
				StateManager.instance.SetState(E_GAME_STATE.Stop);
			}
		}
	}
	public override void End() {
		base.End();
		if(autoDownCoroutine != null) {
			StateManager.instance.StopCoroutine(autoDownCoroutine);
		}
	}

	public override void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			GameManager.instance.PlayerMove(-1, 0);
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			GameManager.instance.PlayerMove(1, 0);
		} else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			GameManager.instance.PlayeRota();
		} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			DropState.isPlayerDown = true;
			StateManager.instance.SetState(E_GAME_STATE.Stop);
		}
	}
}