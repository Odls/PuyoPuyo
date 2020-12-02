using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PUYO_TYPE {
	Wall = -2,
	Empty = -1,
	Red,
	Yellow,
	Green,
	Blue,
	Len
}
public enum E_REMOVE_DIRECTION {
	None = -1,
	Start,
	Right,
	Up,
	Left,
	Down
}
public class LinkInfo {
	public E_PUYO_TYPE type;
	public List<LinkRemoveInfo> removeInfos = new List<LinkRemoveInfo>();

	public void Reset() {
		type = E_PUYO_TYPE.Empty;
		removeInfos.Clear();
	}
}
public struct LinkRemoveInfo {
	public int x, y;
	public E_REMOVE_DIRECTION direction;
}

[System.Serializable]
public class ObjPool<T> where T : MonoBehaviour {
	[SerializeField] Transform dropTop;
	[SerializeField] T prefab;
	Queue<T> queue = new Queue<T>();

	public T GetObj() {
		T _obj;

		if (queue.Count > 0) {
			_obj = queue.Dequeue();
		} else {
			_obj = Object.Instantiate(prefab, dropTop);
		}
		_obj.gameObject.SetActive(true);
		return _obj;
	}
	public void CloseObj(T p_obj) {
		p_obj.gameObject.SetActive(false);
		queue.Enqueue(p_obj);
	}
}
[System.Serializable] public class PuyoDropPool : ObjPool<PuyoDropView> {}
[System.Serializable] public class PuyoRemovePool : ObjPool<PuyoRemoveView> { }
public class BoardManager : ManagerBase<BoardManager> {
	public const int boardWidth = 6;
	public const int boardHeight = 14;
	public const int dieHeight = 12;
	public const float cellSize = 0.64f;
	public const int removeCount = 4;

	[SerializeField] float mPlayerDownDelay = 1f;
	public float playerDownDelay => mPlayerDownDelay;

	public float playerMoveSpeed => mPlayerMoveSpeed;

	#region Player
	[SerializeField] PlayerView mPlayer;
	public PlayerView player => mPlayer;
	#endregion

	#region Board
	[SerializeField] BoardView boardView;
	E_PUYO_TYPE[,] cells = new E_PUYO_TYPE[boardWidth, boardHeight];

	private void Start() {
		for (int _y = 0; _y < boardHeight; _y++) {
			for (int _x = 0; _x < boardWidth; _x++) {
				cells[_x, _y] = E_PUYO_TYPE.Empty;
				linkRemoveInfos[_x, _y] = new LinkRemoveInfo { x = _x, y = _y, direction=E_REMOVE_DIRECTION.None };
			}
		}
		boardView.Init();
		ShowRealCells();
	}
	public void SetViewType(int p_x, int p_y, E_PUYO_TYPE p_type) {
		boardView.SetType(p_x, p_y, p_type);
	}
	public void RefreshView() {
		boardView.Refresh();
	}
	public void ShowRealCells() {
		boardView.SetTypes(cells);
		boardView.Refresh();
	}
	public bool SetCell(int p_x, int p_y, E_PUYO_TYPE p_type) {
		if((p_x >= 0) && (p_x < boardWidth) && (p_y >= 0) && (p_y < boardHeight)) {
			cells[p_x, p_y] = p_type;
			return true;
		} else {
			return false;
		}
	}
	public E_PUYO_TYPE GetCell(int p_x, int p_y) {
		if((p_x<0) || (p_x>=boardWidth) || (p_y<0)) {
			return E_PUYO_TYPE.Wall;
		} else if(p_y>=boardHeight){
			return E_PUYO_TYPE.Empty;
		} else {
			return cells[p_x, p_y];
		}
	}
	public bool IsEmpty(int p_x, int p_y) {
		return GetCell(p_x, p_y) == E_PUYO_TYPE.Empty;
	}
	#endregion

	#region Drop
	[SerializeField] float mDropSpeed = 3f;
	public float dropSpeed => mDropSpeed;
	[SerializeField] float mPlayerDownSpeed = 10f;
	public float playerDownSpeed => mPlayerDownSpeed;
	[SerializeField] float mPlayerMoveSpeed = 5f;

	List<DropInfo> dropInfoList = new List<DropInfo>();
	public List<DropInfo> DoDrop() {
		dropInfoList.Clear();
		for (int _x = 0; _x < boardWidth; _x++) {
			int _emptyY = -1;
			for (int _y = 0; _y < boardHeight; _y++) {
				E_PUYO_TYPE _cellType = instance.GetCell(_x, _y);
				if (_cellType == E_PUYO_TYPE.Empty) {
					if (_emptyY < 0) {
						_emptyY = _y;
					}
				} else {
					if (_emptyY >= 0) {
						SetCell(_x, _emptyY, _cellType);
						SetCell(_x, _y, E_PUYO_TYPE.Empty);

						dropInfoList.Add(
							new DropInfo { x = _x, startY = _y, endY = _emptyY, type= _cellType }
						);

						_emptyY++;
					}
				}
			}
		}
		return dropInfoList;
	}

	public PuyoDropView GetDropPuyo() => boardView.puyoDropPool.GetObj();
	public void CloseDropPuyo(PuyoDropView p_dropPuyo) => boardView.puyoDropPool.CloseObj(p_dropPuyo);
	#endregion

	#region RemoveLink
	Queue<LinkRemoveInfo> tempRemoveInfoQueue = new Queue<LinkRemoveInfo>();
	LinkRemoveInfo[,] linkRemoveInfos = new LinkRemoveInfo[boardWidth, boardHeight];
	List<LinkInfo> linkInfoList = new List<LinkInfo>();
	public List<LinkInfo> DoRemoveLink() {
		linkInfoList.Clear();

		for (int _x = 0; _x < boardWidth; _x++) {
			for (int _y = 0; _y < dieHeight; _y++) {
				linkRemoveInfos[_x, _y].direction = E_REMOVE_DIRECTION.None;
			}
		}

		LinkInfo _linkInfo = new LinkInfo();
		for (int _x = 0; _x < boardWidth; _x++) {
			for (int _y = 0; _y < dieHeight; _y++) {
				if(cells[_x, _y] != E_PUYO_TYPE.Empty) {
					LinkRemoveInfo _nowInfo = linkRemoveInfos[_x, _y];
					if (_nowInfo.direction == E_REMOVE_DIRECTION.None) {
						_linkInfo.Reset();
						_linkInfo.type = cells[_x, _y];
						tempRemoveInfoQueue.Clear();

						AddRemoveInfo(_linkInfo, _x, _y, E_REMOVE_DIRECTION.Start, tempRemoveInfoQueue);

						while (tempRemoveInfoQueue.Count > 0) {
							CheckLink(_linkInfo, tempRemoveInfoQueue);
						}

						if(_linkInfo.removeInfos.Count >= removeCount) {
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
		if((p_x >= 0) && (p_x < boardWidth) && (p_y >= 0) && (p_y < dieHeight)) {
			if (cells[p_x, p_y] == p_linkInfo.type) {
				LinkRemoveInfo _nowInfo = linkRemoveInfos[p_x, p_y];
				if(_nowInfo.direction == E_REMOVE_DIRECTION.None) {
					_nowInfo.direction = p_direction;
					linkRemoveInfos[p_x, p_y] = _nowInfo;
					p_removeInfoQueue.Enqueue(_nowInfo);
					p_linkInfo.removeInfos.Add(_nowInfo);
				}
			}
		}
	}

	void RemoveByInfo() {
		foreach(var _linkInfo in linkInfoList) {
			foreach (var _removeInfo in _linkInfo.removeInfos) {
				SetCell(_removeInfo.x, _removeInfo.y, E_PUYO_TYPE.Empty);
			}
		}
	}

	public PuyoRemoveView GetRemovePuyo() => boardView.puyoRemovePool.GetObj();
	public void CloseRemovePuyo(PuyoRemoveView p_removePuyo) => boardView.puyoRemovePool.CloseObj(p_removePuyo);
	#endregion



	public void RemoveOutRangePuyo() {
		for (int y = dieHeight; y < boardHeight; y++) {
			for (int x = 0; x < boardWidth; x++) {
				cells[x, y] = E_PUYO_TYPE.Empty;
			}
		}
	}
}
