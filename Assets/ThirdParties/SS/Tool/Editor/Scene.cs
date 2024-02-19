﻿// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using System.Collections;

namespace SS.Tool
{
    public class Scene
    {
        public static void OpenScene(string fullScenePath)
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.OpenScene(fullScenePath);
#else
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(fullScenePath);
#endif

#endif
        }

        public static void SaveScene()
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.SaveScene();
#else
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
#endif

#endif
        }

        public static void MarkCurrentSceneDirty()
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.MarkSceneDirty();
#else
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
#endif

#endif
        }
    }
}