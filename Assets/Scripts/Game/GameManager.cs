using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager> {
	StateMachine stateMachine = new StateMachine();

	private void Start() {
		boardView.Init();
		boardView.SetCells(cells);
		boardView.Refresh();

		stateMachine.AddState(new PuyoInState(stateMachine));

		stateMachine.SetState(E_GAME_STATE.PuyoIn);
	}

	#region Player
	[SerializeField] PlayerView player;

	public int playerX { get; private set; }
	public int playerY { get; private set; }
	public bool enablePlayer { set => player.gameObject.SetActive(value); }


	E_MOVING_PUYO_DIRECTION mDirection;
	public E_MOVING_PUYO_DIRECTION direction {
		get => mDirection;
		set {
			mDirection = value;
			player.direction = value;
		}
	}

	public void SetPlayerPos(int p_x, int p_y) {
		playerX = p_x;
		playerY = p_y;
		player.MoveTo(p_x, p_y, true);
	}
	public bool PlayerMove(int p_x, int p_y) {
		playerX += p_x;
		playerY += p_y;

		if (cells.IsEmpty(puyo1X, puyo1Y) && cells.IsEmpty(puyo2X, puyo2Y)) {
			player.MoveTo(playerX, playerY);
			return true;
		} else {
			playerX -= p_x;
			playerY -= p_y;
			return false;
		}
	}
	#endregion

	#region Puyo
	E_PUYO_TYPE type1, type2;

	static int[] puyo2OffsetX = new int[] { 1, 0, -1, 0 };
	static int[] puyo2OffsetY = new int[] { 0, 1, 0, -1 };

	int puyo1X => playerX;
	int puyo1Y => playerY;
	int puyo2X => playerX + puyo2OffsetX[(int)direction];
	int puyo2Y => playerY + puyo2OffsetY[(int)direction];

	public void SetPlayerType(E_PUYO_TYPE p_type1, E_PUYO_TYPE p_type2) {
		type1 = p_type1;
		type2 = p_type2;
		player.SetType(type1, type2);
	}
	public void RandomType() {
		var _typee1 = PuyoManager.GetRandomType();
		var _typee2 = PuyoManager.GetRandomType();
		SetPlayerType(_typee1, _typee2);
	}
	public bool PlayeRota() {
		for (int f = 0; f < 4; f++) {
			mDirection = (E_MOVING_PUYO_DIRECTION)(((int)direction + 1) % 4);
			if (cells.IsEmpty(puyo1X, puyo1Y) && cells.IsEmpty(puyo2X, puyo2Y)) {
				direction = mDirection;
				return true;
			} else if (direction == E_MOVING_PUYO_DIRECTION.Down) {
				if (PlayerMove(0, 1)) {
					direction = mDirection;
					return true;
				}
			}
		}
		return false;
	}
	#endregion

	#region Board
	[SerializeField] BoardView boardView;
	BoardCells cells = new BoardCells();

	public void ApplyPlayer() {
		cells.SetCell(puyo1X, puyo1Y, type1);
		cells.SetCell(puyo2X, puyo2Y, type2);
		boardView.SetCells(cells);
		boardView.Refresh();
	}
	#endregion

}
