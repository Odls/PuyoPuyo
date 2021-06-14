using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : ManagerBase<BoardManager> {
	public const int boardWidth = 6;
	public const int boardHeight = 14;
	public const int dieHeight = 12;
	public const float cellSize = 0.64f;
	public const int removeCount = 4;

	[SerializeField] float mDropSpeed = 3f;
	public float dropSpeed => mDropSpeed;

	#region Player
	[SerializeField] float mPlayerMoveSpeed = 5f;
	public float playerMoveSpeed => mPlayerMoveSpeed;

	[SerializeField] float mPlayerDownDelay = 1f;
	public float playerDownDelay => mPlayerDownDelay;

	[SerializeField] float mPlayerDownSpeed = 10f;
	public float playerDownSpeed => mPlayerDownSpeed;
	#endregion


}
