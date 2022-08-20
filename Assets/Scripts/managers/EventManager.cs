using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using voidCallBack;
using ObjectCallBack;

public sealed class EventManager
{

    private static Dictionary<EventType, Delegate> m_Administrator = new Dictionary<EventType, Delegate>();


    /* 查询事件是否被注册 */
    public static bool EventIsSign(EventType type)
    {
        return m_Administrator.ContainsKey(type);
    }


    /// <summary>
    /// 在注册事件前，进行对事件的判断;
    /// </summary> 
    private static void OnRemoveListener(EventType eventType)
    {
        if (m_Administrator[eventType] == null)
        {
            m_Administrator.Remove(eventType);
        }
    }


    private static void OnAddListener(EventType eventType, Delegate callback)
    {
        if (!m_Administrator.ContainsKey(eventType))
        {
            m_Administrator.Add(eventType, null);
        }
        Delegate ThisCallBack = m_Administrator[eventType];
        if (ThisCallBack != null && ThisCallBack.GetType() != callback.GetType())
        {
            throw new Exception("两个委托的类型不一样");
        }
    }



    private static void OnRemoveListener(EventType eventType, Delegate callback)
    {
        if (m_Administrator.ContainsKey(eventType))
        {
            Delegate oldCallBack = m_Administrator[eventType];
            if (oldCallBack == null)
            {
                throw new Exception("该事件没有对应的委托");
            }
            if (oldCallBack.GetType() != callback.GetType())
            {
                throw new Exception("该委托与此事件的委托类型不符合");
            }
        }
        else
        {
            throw new Exception("此事件不存在");
        }
    }
    /************************************************************************************************/
    /******************************添加和移除有参或者无参的事件***************************************/
    /// <summary>
    /// 添加无参监听事件
    /// </summary>
    public static void AddListener(EventType eventType, CallBack callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (CallBack)m_Administrator[eventType] + callback;
    }



    /// <summary>
    /// 移除无参监听事件
    /// </summary>
    public static void RemoveListener(EventType eventType, CallBack callback)
    {
        OnRemoveListener(eventType, callback);
        m_Administrator[eventType] = (CallBack)m_Administrator[eventType] - callback;
    }



    /// <summary>
    /// 添加1参监听事件
    /// </summary>
    public static void AddListener<T>(EventType eventType, voidCallBack.CallBack<T> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (voidCallBack.CallBack<T>)m_Administrator[eventType] + callback;
    }


    public static void AddListener<T>(EventType eventType, ObjectCallBack.CallBack<T> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T>)m_Administrator[eventType] + callback;
    }




    /// <summary>
    /// 移除1参监听事件
    /// </summary>
    public static void RemoveListener<T>(EventType eventType, voidCallBack.CallBack<T> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (voidCallBack.CallBack<T>)m_Administrator[eventType] - callback;
    }



    public static void RemoveListener<T>(EventType eventType, ObjectCallBack.CallBack<T> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T>)m_Administrator[eventType] - callback;
    }


    /// <summary>
    /// 添加2参监听事件
    /// </summary>
    public static void AddListener<T, X>(EventType eventType, voidCallBack.CallBack<T, X> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X>)m_Administrator[eventType] + callback;
    }


    public static void AddListener<T, X>(EventType eventType, ObjectCallBack.CallBack<T, X> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X>)m_Administrator[eventType] + callback;
    }



    /// <summary>
    /// 移除2参监听事件
    /// </summary>
    public static void RemoveListener<T, X>(EventType eventType, voidCallBack.CallBack<T, X> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X>)m_Administrator[eventType] - callback;
    }


    public static void RemoveListener<T, X>(EventType eventType, ObjectCallBack.CallBack<T, X> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X>)m_Administrator[eventType] - callback;
    }


    /// <summary>
    /// 添加3参监听事件
    /// </summary>
    public static void AddListener<T, X, Z>(EventType eventType, voidCallBack.CallBack<T, X, Z> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X, Z>)m_Administrator[eventType] + callback;
    }



    public static void AddListener<T, X, Z>(EventType eventType, ObjectCallBack.CallBack<T, X, Z> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X, Z>)m_Administrator[eventType] + callback;
    }



    /// <summary>
    /// 移除3参监听事件
    /// </summary>
    public static void RemoveListener<T, X, Z>(EventType eventType, voidCallBack.CallBack<T, X, Z> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X, Z>)m_Administrator[eventType] - callback;
    }


    public static void RemoveListener<T, X, Z>(EventType eventType, ObjectCallBack.CallBack<T, X, Z> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X, Z>)m_Administrator[eventType] - callback;
    }



    /// <summary>
    /// 添加4参监听事件
    /// </summary>
    public static void AddListener<T, X, Z, Y>(EventType eventType, voidCallBack.CallBack<T, X, Z, Y> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X, Z, Y>)m_Administrator[eventType] + callback;
    }



    public static void AddListener<T, X, Z, Y>(EventType eventType, ObjectCallBack.CallBack<T, X, Z, Y> callback)
    {
        OnAddListener(eventType, callback);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X, Z, Y>)m_Administrator[eventType] + callback;
    }



    /// <summary>
    /// 移除4参监听事件
    /// </summary>
    public static void RemoveListener<T, X, Z, Y>(EventType eventType, voidCallBack.CallBack<T, X, Z, Y> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (voidCallBack.CallBack<T, X, Z, Y>)m_Administrator[eventType] - callback;
    }


    public static void RemoveListener<T, X, Z, Y>(EventType eventType, ObjectCallBack.CallBack<T, X, Z, Y> callback)
    {
        OnRemoveListener(eventType);
        m_Administrator[eventType] = (ObjectCallBack.CallBack<T, X, Z, Y>)m_Administrator[eventType] - callback;
    }





    /*****************************************************************************/
    /*********************************广播***************************************/
    /// <summary>
    /// 无参广播
    /// </summary>
    public static void Broadcast(EventType eventType)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            CallBack callBack = deleg as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
    }


    public static T Broadcast<T>(EventType eventType)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            ObjectCallBack.CallBack<T> callBack = deleg as ObjectCallBack.CallBack<T>;
            if (callBack != null)
            {
                return callBack();
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
        return default(T);
    }


    /// <summary>
    /// 1参广播
    /// </summary>
    public static void Broadcast<T>(EventType eventType, T arg)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            voidCallBack.CallBack<T> callBack = deleg as voidCallBack.CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
    }



    public static T Broadcast<T, X>(EventType eventType, X arg)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            ObjectCallBack.CallBack<T, X> callBack = deleg as ObjectCallBack.CallBack<T, X>;
            if (callBack != null)
            {
                return callBack(arg);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
        return default(T);
    }




    /// <summary>
    /// 2参广播
    /// </summary>
    public static void Broadcast<T, X>(EventType eventType, T arg1, X arg2)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            voidCallBack.CallBack<T, X> callBack = deleg as voidCallBack.CallBack<T, X>;
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
    }



    public static T Broadcast<T, X, Z>(EventType eventType, X arg1, Z arg2)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            ObjectCallBack.CallBack<T, X, Z> callBack = deleg as ObjectCallBack.CallBack<T, X, Z>;
            if (callBack != null)
            {
                return callBack(arg1, arg2);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
        return default(T);
    }




    /// <summary>
    /// 3参广播
    /// </summary>
    public static void Broadcast<T, X, Z>(EventType eventType, T arg1, X arg2, Z arg3)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            voidCallBack.CallBack<T, X, Z> callBack = deleg as voidCallBack.CallBack<T, X, Z>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
    }



    public static T Broadcast<T, X, Z, Y>(EventType eventType, X arg1, Z arg2, Y arg3)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            ObjectCallBack.CallBack<T, X, Z, Y> callBack = deleg as ObjectCallBack.CallBack<T, X, Z, Y>;
            if (callBack != null)
            {
                return callBack(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
        return default(T);
    }



    /// <summary>
    /// 4参广播
    /// </summary>
    public static void Broadcast<T, X, Z, Y>(EventType eventType, T arg1, X arg2, Z arg3, Y arg4)
    {
        Delegate deleg;
        if (m_Administrator.TryGetValue(eventType, out deleg))
        {
            voidCallBack.CallBack<T, X, Z, Y> callBack = deleg as voidCallBack.CallBack<T, X, Z, Y>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception("该事件没有注册方法");
            }
        }
    }
}
