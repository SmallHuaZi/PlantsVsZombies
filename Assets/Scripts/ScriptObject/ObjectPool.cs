using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectPool", menuName = "CreateObjectPool")]
public class ObjectPool : ScriptableObject
{
    private Dictionary<string, Queue<PoolObject>> m_Mapping = new Dictionary<string, Queue<PoolObject>>();


    public virtual void RegisterObject(PoolObject obj)
    {

    }


    public virtual PoolObject GetPoolObject(string name)
    {
        PoolObject retObj = null;
        return retObj;
    }
}
