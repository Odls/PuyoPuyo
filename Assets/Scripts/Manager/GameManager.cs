using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager> {
	E_PUYO_TYPE type1, type2;

	E_MOVING_PUYO_DIRECTION mDirection;
	public E_MOVING_PUYO_DIRECTION direction {
		get => mDirection;
		set {
			mDirection = value;
			BoardManager.instance.player.direction = value;
		}
	}

	#region Pos
	static int[] puyo2OffsetX = new int[] { 1, 0, -1, 0 };
	static int[] puyo2OffsetY = new int[] { 0, 1, 0, -1 };

	int playerX, playerY;
	int puyo1X => playerX;
	int puyo1Y => playerY;
	int puyo2X => playerX + puyo2OffsetX[(int)direction];
	int puyo2Y => playerY + puyo2OffsetY[(int)direction];
	#endregion

	private void Start() {
		StateManager.instance.AddState(E_GAME_STATE.PuyoIn, new PuyoInState());
		StateManager.instance.AddState(E_GAME_STATE.Move, new MoveState());
		StateManager.instance.AddState(E_GAME_STATE.Stop, new StopState());
		StateManager.instance.AddState(E_GAME_STATE.Drop, new DropState());
		StateManager.instance.AddState(E_GAME_STATE.Remove, new RemoveState());
		StateManager.instance.AddState(E_GAME_STATE.End, new EndState());

		StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
	}

	public void SetType(E_PUYO_TYPE p_type1, E_PUYO_TYPE p_type2) {
		type1 = p_type1;
		type2 = p_type2;
		BoardManager.instance.player.SetType(type1, type2);
	}
	public void SetPos(int p_x, int p_y) {
		playerX = p_x;
		playerY = p_y;
		BoardManager.instance.player.MoveTo(p_x, p_y, true);
	}
	public bool PlayerMove(int p_x, int p_y) {
		playerX += p_x;
		playerY += p_y;

		if(BoardManager.instance.IsEmpty(puyo1X, puyo1Y) && BoardManager.instance.IsEmpty(puyo2X, puyo2Y)) {
			BoardManager.instance.player.MoveTo(playerX, playerY);
			return true;
		} else {
			playerX -= p_x;
			playerY -= p_y;
			return false;
		}
	}
	public bool PlayeRota() {
		for(int f=0; f < 4; f++) {
			mDirection = (E_MOVING_PUYO_DIRECTION)(((int)direction + 1) % 4);
			if (BoardManager.instance.IsEmpty(puyo1X, puyo1Y) && BoardManager.instance.IsEmpty(puyo2X, puyo2Y)) {
				direction = mDirection;
				return true;
				break;
			} else if(direction == E_MOVING_PUYO_DIRECTION.Down) {
				if(PlayerMove(0, 1)) {
					direction = mDirection;
					return true;
				}
			}
		}
		return false;
	}
	public void ApplyPlayer() {
		BoardManager.instance.SetCell(puyo1X, puyo1Y, type1);
		BoardManager.instance.SetCell(puyo2X, puyo2Y, type2);
		BoardManager.instance.RefreshView();
	}
}
