using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    static T instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }
}
