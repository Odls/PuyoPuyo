using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoDropView : PuyoView
{
    [SerializeField] Animator animator;

    WaitForSeconds waitHitAnimation;
    private void Awake()
    {
        waitHitAnimation = new WaitForSeconds(0.5f);
    }

    public Coroutine Drop(DropInfo p_dropInfo) {
		return BoardManager.instance.StartCoroutine(IeDrop(p_dropInfo));
	}

    IEnumerator IeDrop(DropInfo p_dropInfo)
    {
		SetType(p_dropInfo.type);
		float _targetX = p_dropInfo.x * BoardManager.cellSize;
        float _targetY = p_dropInfo.endY * BoardManager.cellSize;
        float _y = p_dropInfo.startY * BoardManager.cellSize;
		transform.localPosition = new Vector3(_targetX, _y, 0);

		while (_y > _targetY)
        {
            _y = Mathf.MoveTowards(_y, _targetY, BoardManager.instance.dropSpeed * Time.deltaTime);
            transform.localPosition = new Vector3(_targetX, _y, 0);
            yield return null;
        }

        animator.SetInteger("hitNumber", Random.Range(1, 4));

        animator.Play("Hit");
        yield return waitHitAnimation;
    }

	public void Close() {
		if (gameObject.activeSelf) {
			BoardManager.instance.CloseDropPuyo(this);
		}
	}
}
