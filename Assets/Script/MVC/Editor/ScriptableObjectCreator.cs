﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreator  {

    [MenuItem("Assets/Create/Create ScriptableObject")]
    public static void CreateMultipleScriptableObjectAsset()
    {

        ScriptableObject obj = null;

        for (int i = 0; i < Selection.objects.Length; i++)
        {
            Object currentSelectObject = Selection.objects[i];


            if (currentSelectObject is MonoScript)
            {
                MonoScript script = (MonoScript)currentSelectObject;
                System.Type type = script.GetClass();
                if (type.IsSubclassOf(typeof(ScriptableObject)))
                {
                    obj = ScriptableObject.CreateInstance(type.ToString());
                }
            }

            if (obj != null)
            {
                string path = AssetDatabase.GetAssetPath(currentSelectObject);
                path = path.Replace(Path.GetFileName(path), string.Empty);
                path = AssetDatabase.GenerateUniqueAssetPath(path + Path.AltDirectorySeparatorChar + currentSelectObject.name + ".asset");
                AssetDatabase.CreateAsset(obj, path);
                // Selection.activeObject = obj;
            }

        }

    }

    public static T Create<T>(string path) where T : ScriptableObject
    {
        var data = ScriptableObject.CreateInstance<T>();
        if (Directory.Exists(path))
        {
            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssets();
            Selection.activeObject = data;
        }
        return data;
    }
}
