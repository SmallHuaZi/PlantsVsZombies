using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


/* AB包的卸载由此类直接管理，当场中无引用时，自动卸载  */
public class ABManager : Singleton<ABManager>
{
    /* 引用计数类 */
    protected class ReferenceCount
    {
        /* AB包 */
        private AssetBundle m_Asset;
        public AssetBundle Asset
        {
            get => this.m_Asset;
            set
            {
                if (this.m_Asset != null)
                    return;
                this.m_Asset = value;
            }
        }


        /* 引用数 */
        private uint m_RefCount;

        /* 添加引用 */
        public void AddRef() => this.m_RefCount++;

        /* 移除引用 */
        public void RemoveRef()
        {
            if (this.m_RefCount.Equals(0))
                this.m_Asset.Unload(true);
            --this.m_RefCount;
        }
    }


    /* 依赖获取成员 */
    private AssetBundleManifest m_Dependencies = default;
    public AssetBundleManifest Dependencies
    {
        get
        {
            if (this.m_Dependencies == null)
            {
                AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetsBundle/AssetsBundle");
                this.m_Dependencies = assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
            }
            return this.m_Dependencies;
        }
    }


    /* 引用计数映射 */
    private Dictionary<string, ReferenceCount> m_RefCoutMapping = new Dictionary<string, ReferenceCount>();


    /* AB包和AB包对应的依赖 */
    private Dictionary<string, string[]> m_ABDependencies = new Dictionary<string, string[]>();


    /* AB包路径 */
    public string ABPackagePath
    {
        get
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            return Application.streamingAssetsPath + "/AssetsBundle";
#elif UNITY_ANDROID || UNITY_IOS
            return Application.persistentDataPath + "/AssetsBundle";
#endif
        }
    }


    /* AB包对应昵称 */
    private ReferenceCount GetABPackage(ABType type)
    {
        switch (type)
        {
            case ABType.ABPackage_UI:
                return this.LoadAssetBundle("uiobject.u3d");
            case ABType.ABPackage_Run:
                return this.LoadAssetBundle("unity.u3d");
            case ABType.ABPackage_Audio:
                return this.LoadAssetBundle("audio.u3d");
        }
        Debug.LogError("不存在这个AB包");
        return null;
    }


    /* 加载AB包 */
    private ReferenceCount LoadAssetBundle(string Name)
    {
        ReferenceCount refCout = default;
        if (!this.m_RefCoutMapping.ContainsKey(Name))
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(this.ABPackagePath + $"/{Name}");
            if (assetBundle == null)
            {
                Debug.LogError("加载失败!");
                goto _END_;
            }
            refCout = new ReferenceCount();
            refCout.Asset = assetBundle;
            refCout.AddRef();

            /* 加入字典 */
            this.m_RefCoutMapping.Add(Name, refCout);
            string[] depens = this.Dependencies.GetDirectDependencies(assetBundle.name);
            this.m_ABDependencies.Add(Name, depens);
        }
        else
            refCout = this.m_RefCoutMapping[Name];
        _END_: return refCout;
    }



    /* 加载单个资源 */
    public T LoadAsset<T>(ABType type, string Name) where T : UnityEngine.Object
    {
        T retValue = default;
        ReferenceCount refCout = default;
        refCout = this.GetABPackage(type);
        retValue = refCout.Asset.LoadAsset<T>(Name);
        refCout.AddRef();
        return retValue;
    }


    public T[] LoadAllAssets<T>(ABType type) where T : UnityEngine.Object
    {
        T[] retValue = default;
        ReferenceCount refCout = default;
        refCout = this.GetABPackage(type);
        retValue = refCout.Asset.LoadAllAssets<T>();
        refCout.AddRef();
        return retValue;
    }


    public void UnLoad(string ABName)
    {
        this.m_RefCoutMapping[ABName].RemoveRef();
    }

}
