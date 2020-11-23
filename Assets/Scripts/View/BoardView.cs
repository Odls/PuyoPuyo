using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour {
	[SerializeField] PuyoView puyoPrefab;
	[SerializeField] Transform puyoTop;

	int width, heigh;

	PuyoView[,] puyos;

	public void Init(int p_w, int p_h) {
		width = p_w;
		heigh = p_h;
		puyos = new PuyoView[p_w, p_h];

		for (int y = 0; y < p_h; y++) {
			for (int x = 0; x < p_w; x++) {
				PuyoView _puyo = Instantiate(
					puyoPrefab,
					puyoTop
				);

				_puyo.transform.localPosition = new Vector3(x * 0.64f, y * 0.64f, 0);

				puyos[x, y] = _puyo;

			}
		}
	}

	#region Refresh
	public void Refresh(E_TYPE[,] p_board) {
		CheckLink(p_board);
		SetTypes(p_board);
	}

	void SetTypes(E_TYPE[,] p_board) {
		for (int y = 0; y < heigh; y++) {
			for (int x = 0; x < width; x++) {
				PuyoView _puyo = puyos[x, y];
				_puyo.SetType(p_board[x, y]);

			}
		}
	}

	const int rightLinkFlag	= 1;
	const int upLinkFlag	= 1<<1;
	const int leftLinkFlag	= 1<<2;
	const int downLinkFlag	= 1<<3;

	void CheckLink(E_TYPE[,] p_board) {
		// Clear
		for (int y = 0; y < heigh; y++) {
			for (int x = 0; x < width; x++) {
				puyos[x, y].ClearLink();
			}
		}

		// CheckLink
		for (int y = 0; y < heigh; y++) {
			for (int x = 0; x < width; x++) {
				// Check Left
				if (x > 0) {
					if (p_board[x - 1, y] == p_board[x, y]) {
						puyos[x - 1, y].AddLink(rightLinkFlag);
						puyos[x, y].AddLink(leftLinkFlag);
					}
				}
				// Check Down
				if (y > 0) {
					if (p_board[x, y - 1] == p_board[x, y]) {
						puyos[x, y - 1].AddLink(upLinkFlag);
						puyos[x, y].AddLink(downLinkFlag);
					}
				}
			}
		}
	}
	#endregion
}
