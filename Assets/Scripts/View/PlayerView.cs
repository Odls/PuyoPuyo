﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_MOVING_PUYO_DIRECTION {
	Right,
	Up,
	Left,
	Down
}

public class PlayerView : MonoBehaviour {
	static Vector3[] puyo2Pos = new Vector3[] {
		new Vector3(BoardManager.cellSize,0,0),
		new Vector3(0,BoardManager.cellSize,0),
		new Vector3(-BoardManager.cellSize,0,0),
		new Vector3(0,-BoardManager.cellSize,0)
	};

	[SerializeField] PuyoView puyo1, puyo2;
	
	public E_MOVING_PUYO_DIRECTION direction {
		set {
			puyo2.transform.localPosition = puyo2Pos[(int)value];
		}
	}

	private void Awake() {
		direction = E_MOVING_PUYO_DIRECTION.Up;
	}
	
	public void SetType(E_PUYO_TYPE p_type1, E_PUYO_TYPE p_type2) {
		puyo1.SetType(p_type1);
		puyo2.SetType(p_type2);
	}
	public void SetPos(int p_x, int p_y) {
		transform.localPosition = new Vector3(BoardManager.cellSize*p_x, BoardManager.cellSize*p_y, 0);
	}
}
