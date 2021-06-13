using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoView : MonoBehaviour {
	[SerializeField] SpriteRenderer renderer;
	protected virtual int linkFlag => 0;
	public E_PUYO_TYPE type { get; protected set; }
	public void SetType(E_PUYO_TYPE p_type, bool p_autoRefresh = true) {
		type = p_type;
		if (p_autoRefresh) { Refresh(); }
	}
	public void Refresh() {
		PuyoData _puyoData = PuyoManager.instance.GetData(type);
		renderer.sprite = _puyoData.GetSpriteByFlag(linkFlag);
	}
}
