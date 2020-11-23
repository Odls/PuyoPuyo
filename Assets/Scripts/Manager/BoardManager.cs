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
	public const int boardHeight = 12;
	public const float cellSize = 0.64f;

	[SerializeField] float mPlayerDownDelay = 1f;
	public float playerDownDelay => mPlayerDownDelay;

	[SerializeField] BoardView boardView;
	[SerializeField] PlayerView mPlayer;
	public PlayerView player => mPlayer;

	E_PUYO_TYPE[,] cells = new E_PUYO_TYPE[boardWidth, boardHeight];

	private void Start() {
		for (int y = 0; y < boardHeight; y++) {
			for (int x = 0; x < boardWidth; x++) {
				cells[x, y] = E_PUYO_TYPE.Empty;
			}
		}
		boardView.Init();
		RefreshView();
	}
	public void RefreshView() {
		boardView.Refresh(cells);
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
}
