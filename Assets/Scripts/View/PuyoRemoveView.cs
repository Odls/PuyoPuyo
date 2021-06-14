using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoRemoveView : PuyoView {
	[SerializeField] Animator animator;

	WaitForSeconds waitRemoveAnimation;
	private void Awake() {
		waitRemoveAnimation = new WaitForSeconds(1.5f);
	}

	public Coroutine removeCoroutine { get; private set; }
	public Coroutine Remove(E_PUYO_TYPE p_type, LinkRemoveInfo p_removeInfo) {
		if (removeCoroutine != null) {
			BoardManager.instance.StopCoroutine(removeCoroutine);
		}
		removeCoroutine = BoardManager.instance.StartCoroutine(IeRemove(p_type, p_removeInfo));
		return removeCoroutine;
	}

	IEnumerator IeRemove(E_PUYO_TYPE p_type, LinkRemoveInfo p_removeInfo) {
		animator.speed = 0;
		SetType(p_type);
		float _x = p_removeInfo.x * BoardManager.cellSize;
		float _y = p_removeInfo.y * BoardManager.cellSize;
		transform.localPosition = new Vector3(_x, _y, 0);

		float _delayTime = Random.Range(0f, 0.3f);
		while (_delayTime > 0) {
			_delayTime -= Time.deltaTime;
			yield return null;
		}

		animator.speed = 1;
		yield return waitRemoveAnimation;
	}

	public void Close() {
		if (removeCoroutine != null) {
			BoardManager.instance.StopCoroutine(removeCoroutine);
			removeCoroutine = null;
		}
	}
}
