using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Remove;

    WaitForSeconds waitRemoveAnimation;
	public override void Init() {
		base.Init();
		waitRemoveAnimation = new WaitForSeconds(0.5f);
	}

	public override void Start() {
		base.Start();
		removeAllCoroutine = StateManager.instance.StartCoroutine(IeRemoveAll());
	}
	Coroutine removeAllCoroutine = null;
	IEnumerator IeRemoveAll() {
		BoardManager.instance.RemoveOutRangePuyo();
		BoardManager.instance.ShowRealCells();

		List<LinkInfo> _linkInfos = BoardManager.instance.DoRemoveLink();
		BoardManager.instance.ShowRealCells();

		if(_linkInfos.Count > 0) {
			foreach (LinkInfo _linkInfo in _linkInfos) {
				foreach (LinkRemoveInfo _removeInfo in _linkInfo.removeInfos) {
					PuyoRemoveView _removePuyo = BoardManager.instance.GetRemovePuyo();
					_removePuyo.Remove(_linkInfo.type, _removeInfo);
				}
			}

			yield return waitRemoveAnimation;


			StateManager.instance.SetState(E_GAME_STATE.Drop);
		} else {
			StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
		}
	}
	public override void End() {
		base.End();

		if (removeAllCoroutine != null) {
			StateManager.instance.StopCoroutine(removeAllCoroutine);
		}
	}
}