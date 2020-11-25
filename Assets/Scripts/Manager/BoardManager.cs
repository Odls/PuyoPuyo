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

public class BoardManager : ManagerBase<BoardManager> {
	public const int boardWidth = 6;
	public const int boardHeight = 14;
	public const float cellSize = 0.64f;

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
		for (int y = 0; y < boardHeight; y++) {
			for (int x = 0; x < boardWidth; x++) {
				cells[x, y] = E_PUYO_TYPE.Empty;
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

	#region DropObjectPool
	[SerializeField] Transform dropTop;
	[SerializeField] PuyoDropView puyoDropPrefab;
	Queue<PuyoDropView> dropQueue = new Queue<PuyoDropView>();

	public PuyoDropView GetDropPuyo() {
		PuyoDropView _dropPuyo;

		if (dropQueue.Count > 0) {
			_dropPuyo = dropQueue.Dequeue();
		} else {
			_dropPuyo = Instantiate(puyoDropPrefab, dropTop);
		}
		_dropPuyo.gameObject.SetActive(true);
		return _dropPuyo;
	}
	public void CloseDropPuyo(PuyoDropView p_dropPuyo) {
		p_dropPuyo.gameObject.SetActive(false);
		dropQueue.Enqueue(p_dropPuyo);
	}
	#endregion

	#region Drop
	[SerializeField] float mDropSpeed = 3f;
	public float dropSpeed => mDropSpeed;
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
	#endregion


}
