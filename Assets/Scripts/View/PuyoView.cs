using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoView : MonoBehaviour {
	[SerializeField] SpriteRenderer renderer;
	protected virtual int linkFlag => 0;


	public void SetType(E_PUYO_TYPE p_type) {
		PuyoData _puyoData = PuyoManager.instance.GetData(p_type);

		renderer.sprite = _puyoData.GetSpriteByFlag(linkFlag);
	}

}
