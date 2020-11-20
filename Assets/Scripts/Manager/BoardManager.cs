using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TYPE
{
    None,
    R,
    Y,
    G,
    B
}

public class BoardManager : ManagerBase<BoardManager>
{
    const int boardW = 6;
    const int boardH = 12;

    [SerializeField] BoardView boardView;

    E_TYPE[,] board = new E_TYPE[boardW, boardH];

    private void Start()
    {
        board[0, 5] = E_TYPE.R;
        board[3, 7] = E_TYPE.G;
        board[1, 10] = E_TYPE.B;
        board[2, 10] = E_TYPE.B;
        board[2, 11] = E_TYPE.B;

        boardView.Init(boardW, boardH);
        boardView.Refresh(board);

    }
}
