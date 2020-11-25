using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoDropView : PuyoView
{
    public Coroutine Drop(int p_x, int p_startY, int p_endY)
    {
        return StartCoroutine(IeDrop(p_x, p_startY, p_endY));
    }

    IEnumerator IeDrop(int p_x, int p_startY, int p_endY)
    {
        float _targetX = p_x * BoardManager.cellSize;
        float _targetY = p_endY * BoardManager.cellSize;
        float _y = p_startY;

        while (_y > _targetY)
        {
            _y = Mathf.MoveTowards(_y, _targetY, BoardManager.instance.dropSpeed * Time.deltaTime);
            transform.localPosition = new Vector3(_targetX, _y, 0);
            yield return null;
        }
    }
}
