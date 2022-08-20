
/* 无返回值回调委托 */
namespace voidCallBack
{
    public delegate void CallBack();
    public delegate void CallBack<T>(T arg);
    public delegate void CallBack<T, X>(T arg1, X arg2);
    public delegate void CallBack<T, X, Z>(T arg1, X arg2, Z arg3);
    public delegate void CallBack<T, X, Z, Y>(T arg1, X arg2, Z arg3, Y arg4);
}


/* 泛型返回值回调委托 */
namespace ObjectCallBack
{
    public delegate T CallBack<T>();
    public delegate T CallBack<T, X>(X arg2);
    public delegate T CallBack<T, X, Z>(X arg2, Z arg3);
    public delegate T CallBack<T, X, Z, Y>(X arg2, Z arg3, Y arg4);
}


