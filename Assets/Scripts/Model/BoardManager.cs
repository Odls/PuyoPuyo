using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : ManagerBase<BoardManager> {
	public const int boardWidth = 6;
	public const int boardHeight = 14;
	public const int dieHeight = 12;
	public const float cellSize = 0.64f;
	public const int removeCount = 4;

	#region Player
	[SerializeField] float mPlayerMoveSpeed = 5f;
	public float playerMoveSpeed => mPlayerMoveSpeed;
	#endregion


}
