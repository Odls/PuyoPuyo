using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_REMOVE_DIRECTION {
	None = -1,
	Start,
	Right,
	Up,
	Left,
	Down
}
public class LinkInfo {
	public E_PUYO_TYPE type;
	public List<LinkRemoveInfo> removeInfos = new List<LinkRemoveInfo>();

	public void Reset() {
		type = E_PUYO_TYPE.Empty;
		removeInfos.Clear();
	}
}
public struct LinkRemoveInfo {
	public int x, y;
	public E_REMOVE_DIRECTION direction;
}
