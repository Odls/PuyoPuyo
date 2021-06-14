using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropState : StateBase {
	readonly BoardView boardView;
	readonly BoardCells cells;
	public DropState(StateMachine p_stateMachine, BoardView p_boardView, BoardCells p_cells) : base(p_stateMachine) {
		boardView = p_boardView;
		cells = p_cells;
	}

	public override E_GAME_STATE stateEnum => E_GAME_STATE.Drop;

	public override void Start() {
		base.Start();
		dropAllCoroutine = StartCoroutine(IeDropAll());
	}

	Coroutine dropAllCoroutine = null;
	List<PuyoDropView> dropPuyos = new List<PuyoDropView>();
	IEnumerator IeDropAll() {
		List<DropInfo> _dropInfoList = cells.DoDrop();
		foreach (DropInfo _info in _dropInfoList) {
			boardView.SetCell(_info.x, _info.startY, E_PUYO_TYPE.Empty);
		}
		boardView.Refresh();

		foreach (DropInfo _dropInfo in _dropInfoList) {
			PuyoDropView _dropPuyo = boardView.GetDropPuyo();
			dropPuyos.Add(_dropPuyo);
			float _speed = (GameManager.instance.isPlayerDown ? BoardManager.instance.playerDownSpeed : BoardManager.instance.dropSpeed);
			_dropPuyo.Drop(_dropInfo, _speed);
		}
		GameManager.instance.isPlayerDown = false;

		foreach (var _dropPuyo in dropPuyos) {
			yield return _dropPuyo.dropCoroutine;
		}
		boardView.SetCells(cells);

		stateMachine.SetState(E_GAME_STATE.Remove);
	}

	public override void End() {
		base.End();

		foreach (PuyoDropView _dropPuyo in dropPuyos) {
			boardView.CloseDropPuyo(_dropPuyo);
		}
		dropPuyos.Clear();

		StopCoroutine(ref dropAllCoroutine);
	}
}