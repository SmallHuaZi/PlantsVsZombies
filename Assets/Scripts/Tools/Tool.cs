using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Tool
{
    public static GameObject ObjectInitialize(GameObject prefab, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("传入的预制体为空");
            return null;
        }
        GameObject LoadGo = GameObject.Instantiate(prefab);

        LoadGo.name = prefab.name;
        if (LoadGo && parent)
        {
            RectTransform GoRect = LoadGo.transform as RectTransform;

            Transform Go = LoadGo.transform;
            Go.transform.SetParent(parent);
            Go.localPosition = Vector3.zero;
            Go.localRotation = Quaternion.identity;
            Go.localScale = Vector3.one;

            if (GoRect)
            {
                GoRect.anchoredPosition = Vector3.zero;
                GoRect.localRotation = Quaternion.identity;
                GoRect.localScale = Vector3.one;
                if (GoRect.CompareTag("AllPanel"))
                {
                    GoRect.offsetMax = Vector2.zero;
                    GoRect.offsetMin = Vector2.zero;
                }
            }
        }
        LoadGo.layer = parent.gameObject.layer;
        return LoadGo;
    }


    public static void Log(string sql)
    {
        FileStream logFile = default;
        string LogPath = Application.streamingAssetsPath + "/sqlLog.txt";

        if (!File.Exists(LogPath))
        {
            logFile = File.Create(LogPath);
            logFile.Write(Encoding.UTF8.GetBytes("sql语句日志:\n"), 0, 13);
        }
        else
            logFile = File.Open(LogPath, FileMode.Append, FileAccess.Write);
        if (logFile != null)
        {
#if CODE_DONTCOMPILE
            if (logFile == null)
                Debug.Log("打开日志文件");
#endif
            StringBuilder logInfo = new StringBuilder(DateTime.Now.ToString());
            logInfo.Append(":");
            logInfo.Append(sql);
            logInfo.Append('\n');
            byte[] content = Encoding.UTF8.GetBytes(logInfo.ToString());
            logFile.Write(content, 0, content.Length);
            logFile.Close();
        }

    }
}
