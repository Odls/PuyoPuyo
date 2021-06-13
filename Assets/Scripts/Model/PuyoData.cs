using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PUYO_TYPE {
	Wall = -2,
	Empty = -1,
	Red,
	Yellow,
	Green,
	Blue,
	Len
}

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
