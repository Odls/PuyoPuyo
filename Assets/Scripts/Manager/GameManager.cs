using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    private void Start()
    {
        StateManager.instance.AddState(E_GAME_STATE.PuyoIn, new PuyoInState());
        StateManager.instance.AddState(E_GAME_STATE.Move, new MoveState());
        StateManager.instance.AddState(E_GAME_STATE.Stop, new StopState());

        StateManager.instance.SetState(E_GAME_STATE.PuyoIn);
    }
}
