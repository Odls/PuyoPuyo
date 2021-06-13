using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T> {
	public static T instance { get; private set; }

	protected virtual void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = (T)this;
		}
	}
}
