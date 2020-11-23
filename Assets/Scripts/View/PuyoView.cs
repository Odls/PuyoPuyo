using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoView : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;

    public void SetType(E_TYPE p_type)
    {
		PuyoData _puyoData = PuyoManager.instance.GetData(p_type);

		renderer.sprite = _puyoData.GetSpriteByFlag(linkFlag);
    }

	int linkFlag = 0;
	public void ClearLink() { linkFlag = 0; }
	public void AddLink(int p_linkFlag) { linkFlag |= p_linkFlag; }

}
