﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour {
	[SerializeField] PuyoCellView puyoPrefab;
	[SerializeField] Transform puyoTop;
	public PuyoDropPool puyoDropPool = new PuyoDropPool();
	public PuyoRemovePool puyoRemovePool = new PuyoRemovePool();

	int width = BoardManager.boardWidth;
	int height = BoardManager.boardHeight;

	PuyoCellView[,] puyos;

	public void Init() {
		puyos = new PuyoCellView[width, height];

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				PuyoCellView _puyo = Instantiate(
					puyoPrefab,
					puyoTop
				);

				_puyo.transform.localPosition = new Vector3(x * BoardManager.cellSize, y * BoardManager.cellSize, 0);

				puyos[x, y] = _puyo;

			}
		}
	}

	#region Refresh
	public void Refresh() {
		CheckLink();
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				PuyoView _puyo = puyos[x, y];
				_puyo.Refresh();
			}
		}
	}
	public void SetTypes(E_PUYO_TYPE[,] p_board) {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				SetType(x, y, p_board[x, y]);
			}
		}
	}
	public void SetType(int p_x, int p_y, E_PUYO_TYPE p_type) {
		PuyoView _puyo = puyos[p_x, p_y];
		_puyo.type = p_type;
	}


	const int rightLinkFlag	= 1;
	const int upLinkFlag	= 1<<1;
	const int leftLinkFlag	= 1<<2;
	const int downLinkFlag	= 1<<3;

	void CheckLink() {
		// Clear
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				puyos[x, y].ClearLink();
			}
		}

		// CheckLink
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				// Check Left
				if (x > 0) {
					if (puyos[x - 1, y].type == puyos[x, y].type) {
						puyos[x - 1, y].AddLink(rightLinkFlag);
						puyos[x, y].AddLink(leftLinkFlag);
					}
				}
				// Check Down
				if (y > 0) {
					if (puyos[x, y - 1].type == puyos[x, y].type) {
						puyos[x, y - 1].AddLink(upLinkFlag);
						puyos[x, y].AddLink(downLinkFlag);
					}
				}
			}
		}
	}
	#endregion
}
