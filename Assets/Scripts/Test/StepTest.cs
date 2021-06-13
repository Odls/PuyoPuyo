using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTest : MonoBehaviour{
	private void Start() {
		GameManager.instance.SetPlayerPos(2, 12);
		GameManager.instance.RandomType();
		GameManager.instance.direction = E_MOVING_PUYO_DIRECTION.Up;

	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			GameManager.instance.PlayerMove(-1, 0);
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			GameManager.instance.PlayerMove(1, 0);
		} else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			GameManager.instance.PlayerMove(0, 1);
		} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			GameManager.instance.PlayerMove(0, -1);
		} else if (Input.GetKeyDown(KeyCode.F)) {
			GameManager.instance.PlayeRota();
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.instance.ApplyPlayer();
			GameManager.instance.RandomType();
		}
	}
}
