using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveState : StateBase {
	readonly BoardView boardView;
	readonly BoardCells cells;
	public RemoveState(StateMachine p_stateMachine, BoardView p_boardView, BoardCells p_cells) : base(p_stateMachine) {
		boardView = p_boardView;
		cells = p_cells;
		waitRemoveAnimation = new WaitForSeconds(0.5f);
	}
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Remove;

	WaitForSeconds waitRemoveAnimation;

	Coroutine removeAllCoroutine = null;
	public override void Start() {
		base.Start();
		removeAllCoroutine = StartCoroutine(IeRemoveAll());
	}
	IEnumerator IeRemoveAll() {
		if (cells.CheckHasOutRangePuyo()) {
			stateMachine.SetState(E_GAME_STATE.End);
			yield break;
		}

		List<LinkInfo> _linkInfos = cells.DoRemoveLink();
		boardView.SetCells(cells);

		if (_linkInfos.Count > 0) {
			foreach (LinkInfo _linkInfo in _linkInfos) {
				foreach (LinkRemoveInfo _removeInfo in _linkInfo.removeInfos) {
					PuyoRemoveView _removePuyo = boardView.GetRemovePuyo();
					_removePuyo.Remove(_linkInfo.type, _removeInfo);
				}
			}

			yield return waitRemoveAnimation;


			stateMachine.SetState(E_GAME_STATE.Drop);
		} else {
			stateMachine.SetState(E_GAME_STATE.PuyoIn);
		}
	}
	public override void End() {
		base.End();
		StopCoroutine(ref removeAllCoroutine);
	}
}