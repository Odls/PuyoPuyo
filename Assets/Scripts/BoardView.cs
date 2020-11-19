using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] Puyo puyoPrefab;
    [SerializeField] Transform puyoTop;

    Puyo[,] puyos;

    public void Init(int p_w, int p_h)
    {
        puyos = new Puyo[p_w, p_h];

        for (int y = 0; y < p_h; y++) 
        {
            for (int x = 0; x < p_w; x++)
            {
                puyos[x, y] = Instantiate(
                    puyoPrefab,
                    new Vector3(x * 0.64f, y * 0.64f, 0),
                    Quaternion.identity,
                    puyoTop
                );
            }
        }
    }

    public void Refresh(E_TYPE[,] p_board)
    {

    }
}
