using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T s_Instance = default;
    public static T _Instance { get => s_Instance; }

    protected virtual void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this as T;
            if (s_Instance == null)
                Debug.LogError("创建单例失败");
        }
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
