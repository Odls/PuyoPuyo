﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropInfo {
	public int x, startY, endY;
	public E_PUYO_TYPE type;
}
public class DropState : StateBase {
	public override E_GAME_STATE stateEnum => E_GAME_STATE.Drop;
	public static bool isPlayerDown = true;

	public override void Start() {
		base.Start();

		dropAllCoroutine = StateManager.instance.StartCoroutine(IeDropAll());
	}

	List<Coroutine> dropCoroutines = new List<Coroutine>();
	Coroutine dropAllCoroutine = null;
	List<PuyoDropView> dropPuyos = new List<PuyoDropView>();
	IEnumerator IeDropAll() {
		List<DropInfo> _dropInfoList = BoardManager.instance.DoDrop();
		foreach (DropInfo _info in _dropInfoList) {
			BoardManager.instance.SetViewType(_info.x, _info.startY, E_PUYO_TYPE.Empty);
		}
		BoardManager.instance.RefreshView();

		dropCoroutines.Clear();
		foreach (DropInfo _dropInfo in _dropInfoList) {
			PuyoDropView _dropPuyo = BoardManager.instance.GetDropPuyo();
			dropPuyos.Add(_dropPuyo);
			float _speed = (isPlayerDown ? BoardManager.instance.playerDownSpeed : BoardManager.instance.dropSpeed);
			dropCoroutines.Add(_dropPuyo.Drop(_dropInfo, _speed));
		}
		isPlayerDown = false;

		foreach (Coroutine _dropCoroutine in dropCoroutines) {
			yield return _dropCoroutine;
		}
		BoardManager.instance.ShowRealCells();

		StateManager.instance.SetState(E_GAME_STATE.Remove);
	}

	public override void End() {
		base.End();

		foreach (Coroutine _dropCoroutine in dropCoroutines) {
			StateManager.instance.StopCoroutine(_dropCoroutine);
		}
		dropCoroutines.Clear();

		foreach (PuyoDropView _dropPuyo in dropPuyos) {
			_dropPuyo.Close();
		}
		dropPuyos.Clear();

		if (dropAllCoroutine != null) {
			StateManager.instance.StopCoroutine(dropAllCoroutine);
		}
	}
}