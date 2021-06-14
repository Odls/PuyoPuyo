using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCells {
	static int width => BoardManager.boardWidth;
	static int height => BoardManager.boardHeight;

	#region Cell
	E_PUYO_TYPE[,] cells = new E_PUYO_TYPE[width, height];
	public E_PUYO_TYPE this[int p_x, int p_y] {
		get {
			if ((p_x < 0) || (p_x >= width) || (p_y < 0)) {
				return E_PUYO_TYPE.Wall;
			} else if (p_y >= height) {
				return E_PUYO_TYPE.Empty;
			} else {
				return cells[p_x, p_y];
			}
		}
	}
	public BoardCells() {
		for (int _y = 0; _y < height; _y++) {
			for (int _x = 0; _x < width; _x++) {
				cells[_x, _y] = E_PUYO_TYPE.Empty;
			}
		}
	}
	public bool SetCell(int p_x, int p_y, E_PUYO_TYPE p_type) {
		if ((p_x >= 0) && (p_x < width) && (p_y >= 0) && (p_y < height)) {
			cells[p_x, p_y] = p_type;
			return true;
		} else {
			return false;
		}
	}
	public bool IsEmpty(int p_x, int p_y) {
		return this[p_x, p_y] == E_PUYO_TYPE.Empty;
	}
	#endregion

	#region Drop
	List<DropInfo> dropInfoList = new List<DropInfo>();
	public List<DropInfo> DoDrop() {
		dropInfoList.Clear();
		for (int _x = 0; _x < width; _x++) {
			int _emptyY = -1;
			for (int _y = 0; _y < height; _y++) {
				E_PUYO_TYPE _cellType = this[_x, _y];
				if (_cellType == E_PUYO_TYPE.Empty) {
					if (_emptyY < 0) {
						_emptyY = _y;
					}
				} else {
					if (_emptyY >= 0) {
						SetCell(_x, _emptyY, _cellType);
						SetCell(_x, _y, E_PUYO_TYPE.Empty);

						dropInfoList.Add(
							new DropInfo { x = _x, startY = _y, endY = _emptyY, type = _cellType }
						);

						_emptyY++;
					}
				}
			}
		}
		return dropInfoList;
	}
	#endregion
}
