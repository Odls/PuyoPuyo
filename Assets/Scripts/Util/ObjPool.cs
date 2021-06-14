using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjPool<T> where T : MonoBehaviour {
	[SerializeField] Transform dropTop;
	[SerializeField] T prefab;
	Queue<T> queue = new Queue<T>();

	public T GetObj() {
		T _obj;

		if (queue.Count > 0) {
			_obj = queue.Dequeue();
		} else {
			_obj = Object.Instantiate(prefab, dropTop);
		}
		_obj.gameObject.SetActive(true);
		return _obj;
	}
	public void CloseObj(T p_obj) {
		p_obj.gameObject.SetActive(false);
		queue.Enqueue(p_obj);
	}
}
