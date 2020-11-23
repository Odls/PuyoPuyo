﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PuyoData : ScriptableObject {
	[SerializeField] E_TYPE mType;
	public E_TYPE type { get => mType; }

	[SerializeField] Sprite[] sprites;
	public Sprite GetSpriteByFlag(int p_linkFlag) {
		if (p_linkFlag >= sprites.Length) {
			return null;
		} else {
			return sprites[p_linkFlag];
		}
	}
}
