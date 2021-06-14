using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCells {
	static int width => BoardManager.boardWidth;
	static int height => BoardManager.boardHeight;
	static int dieHeight => BoardManager.dieHeight;
	static int removeCount => BoardManager.removeCount;

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

	public void RemoveOutRangePuyo() {
		for (int y = dieHeight; y < height; y++) {
			for (int x = 0; x < width; x++) {
				cells[x, y] = E_PUYO_TYPE.Empty;
			}
		}
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

	#region RemoveLink
	Queue<LinkRemoveInfo> tempRemoveInfoQueue = new Queue<LinkRemoveInfo>();
	LinkRemoveInfo[,] linkRemoveInfos = new LinkRemoveInfo[width, height];
	List<LinkInfo> linkInfoList = new List<LinkInfo>();
	public List<LinkInfo> DoRemoveLink() {
		linkInfoList.Clear();

		for (int _x = 0; _x < width; _x++) {
			for (int _y = 0; _y < dieHeight; _y++) {
				linkRemoveInfos[_x, _y].direction = E_REMOVE_DIRECTION.None;
			}
		}

		LinkInfo _linkInfo = new LinkInfo();
		for (int _x = 0; _x < width; _x++) {
			for (int _y = 0; _y < dieHeight; _y++) {
				if (cells[_x, _y] != E_PUYO_TYPE.Empty) {
					LinkRemoveInfo _nowInfo = linkRemoveInfos[_x, _y];
					if (_nowInfo.direction == E_REMOVE_DIRECTION.None) {
						_linkInfo.Reset();
						_linkInfo.type = cells[_x, _y];
						tempRemoveInfoQueue.Clear();

						AddRemoveInfo(_linkInfo, _x, _y, E_REMOVE_DIRECTION.Start, tempRemoveInfoQueue);

						while (tempRemoveInfoQueue.Count > 0) {
							CheckLink(_linkInfo, tempRemoveInfoQueue);
						}

						if (_linkInfo.removeInfos.Count >= removeCount) {
							linkInfoList.Add(_linkInfo);
							_linkInfo = new LinkInfo();
						}
					}
				}
			}
		}

		RemoveByInfo();

		return linkInfoList;
	}

	void CheckLink(LinkInfo p_linkInfo, Queue<LinkRemoveInfo> p_removeInfoQueue) {
		LinkRemoveInfo _removeInfo = p_removeInfoQueue.Dequeue();

		switch (_removeInfo.direction) {
		case E_REMOVE_DIRECTION.Start:
			AddRemoveInfo(p_linkInfo, _removeInfo.x - 1, _removeInfo.y, E_REMOVE_DIRECTION.Left, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x + 1, _removeInfo.y, E_REMOVE_DIRECTION.Right, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y + 1, E_REMOVE_DIRECTION.Up, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y - 1, E_REMOVE_DIRECTION.Down, p_removeInfoQueue);
			break;
		case E_REMOVE_DIRECTION.Right:
			AddRemoveInfo(p_linkInfo, _removeInfo.x + 1, _removeInfo.y, E_REMOVE_DIRECTION.Right, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y + 1, E_REMOVE_DIRECTION.Up, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y - 1, E_REMOVE_DIRECTION.Down, p_removeInfoQueue);
			break;
		case E_REMOVE_DIRECTION.Up:
			AddRemoveInfo(p_linkInfo, _removeInfo.x - 1, _removeInfo.y, E_REMOVE_DIRECTION.Left, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x + 1, _removeInfo.y, E_REMOVE_DIRECTION.Right, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y + 1, E_REMOVE_DIRECTION.Up, p_removeInfoQueue);
			break;
		case E_REMOVE_DIRECTION.Left:
			AddRemoveInfo(p_linkInfo, _removeInfo.x - 1, _removeInfo.y, E_REMOVE_DIRECTION.Left, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y + 1, E_REMOVE_DIRECTION.Up, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y - 1, E_REMOVE_DIRECTION.Down, p_removeInfoQueue);
			break;
		case E_REMOVE_DIRECTION.Down:
			AddRemoveInfo(p_linkInfo, _removeInfo.x - 1, _removeInfo.y, E_REMOVE_DIRECTION.Left, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x + 1, _removeInfo.y, E_REMOVE_DIRECTION.Right, p_removeInfoQueue);
			AddRemoveInfo(p_linkInfo, _removeInfo.x, _removeInfo.y - 1, E_REMOVE_DIRECTION.Down, p_removeInfoQueue);
			break;
		}
	}

	void AddRemoveInfo(LinkInfo p_linkInfo, int p_x, int p_y, E_REMOVE_DIRECTION p_direction, Queue<LinkRemoveInfo> p_removeInfoQueue) {
		if ((p_x >= 0) && (p_x < width) && (p_y >= 0) && (p_y < dieHeight)) {
			if (cells[p_x, p_y] == p_linkInfo.type) {
				LinkRemoveInfo _nowInfo = linkRemoveInfos[p_x, p_y];
				if (_nowInfo.direction == E_REMOVE_DIRECTION.None) {
					_nowInfo.direction = p_direction;
					linkRemoveInfos[p_x, p_y] = _nowInfo;
					p_removeInfoQueue.Enqueue(_nowInfo);
					p_linkInfo.removeInfos.Add(_nowInfo);
				}
			}
		}
	}

	void RemoveByInfo() {
		foreach (var _linkInfo in linkInfoList) {
			foreach (var _removeInfo in _linkInfo.removeInfos) {
				SetCell(_removeInfo.x, _removeInfo.y, E_PUYO_TYPE.Empty);
			}
		}
	}

	#endregion
}
