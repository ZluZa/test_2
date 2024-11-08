using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = (T)FindObjectOfType(typeof(T));
        else if (Instance == this)
            return;
        else
            DestroyDuplicate();
    }

    protected virtual void DestroyDuplicate()
    {
        Destroy(this);
    }

    public static T Instance { get; protected set; }
}
