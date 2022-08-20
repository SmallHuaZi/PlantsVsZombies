using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class RemoveMissingScript : Editor
{
    [MenuItem("Test/CleanMissingScript")]
    public static void Begin()
    {
        GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));

        int r;
        int j;
        for (int i = 0; i < pAllObjects.Length; i++)
        {
            if (pAllObjects[i].hideFlags == HideFlags.None)//HideFlags.None 获取Hierarchy面板所有Object
            {
                var components = pAllObjects[i].GetComponents<Component>();
                var serializedObject = new SerializedObject(pAllObjects[i]);
                var prop = serializedObject.FindProperty("m_Component");
                r = 0;

                for (j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        prop.DeleteArrayElementAtIndex(j - r);
                        r++;
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
        AssetDatabase.Refresh();
    }
}
