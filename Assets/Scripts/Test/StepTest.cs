using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTest : MonoBehaviour{
	[SerializeField] PlayerView playerView;

	int x = 0;
	int y = 0;

	private void Start() {
		MovePlayer();
	}
	private void Update() {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			x--;
			MovePlayer();
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			x++;
			MovePlayer();
		} else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			y++;
			MovePlayer();
		} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			y--;
			MovePlayer();
		} else {
			return;
		}


	}

	void MovePlayer() {
		var _typee1 = PuyoManager.instance.GetRandomType();
		var _typee2 = PuyoManager.instance.GetRandomType();
		playerView.SetType(_typee1, _typee2);
		playerView.MoveTo(x, y);
	}
}
