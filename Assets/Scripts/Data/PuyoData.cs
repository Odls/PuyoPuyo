using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PuyoData : ScriptableObject {
	[SerializeField] E_PUYO_TYPE mType;
	public E_PUYO_TYPE type => mType;

	[SerializeField] Sprite[] sprites;
	public Sprite GetSpriteByFlag(int p_linkFlag) {
		if (p_linkFlag >= sprites.Length) {
			return null;
		} else {
			return sprites[p_linkFlag];
		}
	}
}
