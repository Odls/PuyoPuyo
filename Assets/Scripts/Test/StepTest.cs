using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTest : MonoBehaviour{
	[SerializeField] BoardView boardView;
	BoardCells cells = new BoardCells();

	private void Start() {
		for (int _y = 0; _y < BoardManager.boardHeight; _y++) {
			for (int _x = 0; _x < BoardManager.boardWidth; _x++) {
				var _type = PuyoManager.GetRandomType();
				cells.SetCell(_x, _y, _type);
			}
		}

		boardView.Init();
		boardView.SetCells(cells);
		boardView.Refresh();
	}

}
