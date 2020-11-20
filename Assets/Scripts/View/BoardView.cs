using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] Puyo puyoPrefab;
    [SerializeField] Transform puyoTop;

    int width, heigh;

    Puyo[,] puyos;

    public void Init(int p_w, int p_h)
    {
        width = p_w;
        heigh = p_h;
        puyos = new Puyo[p_w, p_h];

        for (int y = 0; y < p_h; y++) 
        {
            for (int x = 0; x < p_w; x++)
            {
                Puyo _puyo = Instantiate(
                    puyoPrefab,
                    puyoTop
                );

                _puyo.transform.localPosition = new Vector3(x * 0.64f, y * 0.64f, 0);

                puyos[x, y] = _puyo;

            }
        }

    }

    public void Refresh(E_TYPE[,] p_board)
    {
        for (int y = 0; y < heigh; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Puyo _puyo = puyos[x, y];
                _puyo.SetType(p_board[x, y]);

            }
        }
    }
}
