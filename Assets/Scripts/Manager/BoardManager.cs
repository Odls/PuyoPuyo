using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TYPE
{
    None,
    Red,
    Yellow,
    Green,
    Blue
}

public class BoardManager : ManagerBase<BoardManager>
{
    const int boardW = 6;
    const int boardH = 12;

    [SerializeField] BoardView boardView;

    E_TYPE[,] board = new E_TYPE[, ] {
		{E_TYPE.Red,	E_TYPE.Red,		E_TYPE.Blue,	E_TYPE.Blue,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None},
		{E_TYPE.Blue,	E_TYPE.Red,		E_TYPE.Red,		E_TYPE.Green,	E_TYPE.Green,	E_TYPE.Green,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None},
		{E_TYPE.Blue,	E_TYPE.Blue,	E_TYPE.Red,		E_TYPE.Green,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None},
		{E_TYPE.Green,	E_TYPE.Blue,	E_TYPE.Blue,	E_TYPE.Blue,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None},
		{E_TYPE.Green,	E_TYPE.Green,	E_TYPE.Green,	E_TYPE.Blue,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None},
		{E_TYPE.Red,	E_TYPE.Green,	E_TYPE.Blue,	E_TYPE.Blue,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None,	E_TYPE.None}
	};

    private void Start()
    {

        boardView.Init(boardW, boardH);
        boardView.Refresh(board);

    }
}
