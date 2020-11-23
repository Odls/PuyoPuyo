using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Drop;

	public override void Start() {
		base.Start();

		bool _hasDrop = false;

		for (int x = 0; x < BoardManager.boardWidth; x++) {
			int _emptyY = -1; 
			for (int y = 0; y < BoardManager.boardHeight; y++) {
				E_PUYO_TYPE _cellType = BoardManager.instance.GetCell(x, y);
				if (_cellType == E_PUYO_TYPE.Empty) {
					if (_emptyY < 0) {
						_emptyY = y;
					}
				} else {
					if (_emptyY >= 0) {
						BoardManager.instance.SetCell(x, _emptyY, _cellType);
						BoardManager.instance.SetCell(x, y, E_PUYO_TYPE.Empty);
						_emptyY++;
						_hasDrop = true;
					}
				}
			}
		}

		if (_hasDrop) {
			BoardManager.instance.RefreshView();
		}

		StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
	}
}