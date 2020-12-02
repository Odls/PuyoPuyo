using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase {
	public abstract E_GAME_STATE stateEnum { get; }

	public virtual void Init() {}
	public virtual void Start() {}
	public virtual void Update() {}
	public virtual void End() {}
}
