using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoDropView : PuyoView {

	public Coroutine dropCoroutine { get; private set; }
	public Coroutine Drop(DropInfo p_dropInfo, float p_dropSpeed) {
		if(dropCoroutine != null) {
			BoardManager.instance.StopCoroutine(dropCoroutine);
		}
		dropCoroutine = BoardManager.instance.StartCoroutine(IeDrop(p_dropInfo, p_dropSpeed));
		return dropCoroutine;
	}



	IEnumerator IeDrop(DropInfo p_dropInfo, float p_dropSpeed) {
		SetType(p_dropInfo.type);
		float _targetX = p_dropInfo.x * BoardManager.cellSize;
		float _targetY = p_dropInfo.endY * BoardManager.cellSize;
		float _y = p_dropInfo.startY * BoardManager.cellSize;
		transform.localPosition = new Vector3(_targetX, _y, 0);

		while (_y > _targetY) {
			_y = Mathf.MoveTowards(_y, _targetY, p_dropSpeed * Time.deltaTime);
			transform.localPosition = new Vector3(_targetX, _y, 0);
			yield return null;
		}
	}

	public void Close() {
		if (dropCoroutine != null) {
			BoardManager.instance.StopCoroutine(dropCoroutine);
			dropCoroutine = null;
		}
	}
}
