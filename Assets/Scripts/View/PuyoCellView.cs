using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoCellView : PuyoView {
	int mLinkFlag = 0;
	protected override int linkFlag => mLinkFlag;
	public void ClearLink() { mLinkFlag = 0; }
	public void AddLink(int p_linkFlag) { mLinkFlag |= p_linkFlag; }
}
