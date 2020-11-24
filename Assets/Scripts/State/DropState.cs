using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropInfo
{
    public int x, startY, endY;
}
public class DropState : StateBase {

    

    public override E_GAME_STATE stateEnum => E_GAME_STATE.Drop;

    List<DropInfo> dropInfoList = new List<DropInfo>();


    public override void Start() {
		base.Start();

		bool _hasDrop = false;

		for (int _x = 0; _x < BoardManager.boardWidth; _x++) {
			int _emptyY = -1; 
			for (int _y = 0; _y < BoardManager.boardHeight; _y++) {
				E_PUYO_TYPE _cellType = BoardManager.instance.GetCell(_x, _y);
				if (_cellType == E_PUYO_TYPE.Empty) {
					if (_emptyY < 0) {
						_emptyY = _y;
					}
				} else {
					if (_emptyY >= 0) {
						BoardManager.instance.SetCell(_x, _emptyY, _cellType);
						BoardManager.instance.SetCell(_x, _y, E_PUYO_TYPE.Empty);

                        dropInfoList.Add(
                            new DropInfo { x = _x, startY = _y, endY = _emptyY }
                        );

                        _emptyY++;
						_hasDrop = true;
					}
				}
			}
		}



		//if (_hasDrop) {
		//	BoardManager.instance.RefreshView();
		//}

		//StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
	}
}