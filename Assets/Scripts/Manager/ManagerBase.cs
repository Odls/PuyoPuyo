using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    private static T mInstance;
    public static T instance { get => mInstance; private set => mInstance = value; }

    protected virtual void Awake()
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
