using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTest : MonoBehaviour{
	[SerializeField] PuyoView puyoView;
	private IEnumerator Start() {
		WaitForSeconds _wait = new WaitForSeconds(0.1f);
		while (true) {
			var _type = PuyoManager.instance.GetRandomType();
			puyoView.SetType(_type);
			yield return _wait;
		}
	}
}
