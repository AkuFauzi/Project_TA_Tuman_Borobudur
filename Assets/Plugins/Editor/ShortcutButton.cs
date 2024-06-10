using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortcutButton : MonoBehaviour
{
    [ToolbarLeft]
    public static void OpenSceneByName(string sceneName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string scenePath = "";
        for (int i = 0; i < sceneCount; i++)
        {
            scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string _sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (_sceneName == sceneName)
                break;
        }
        if (!targetScene.isLoaded)
        {
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
    [ToolbarRight]
    public static void OpenAssets()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Asset"));
    }
    [ToolbarRight]
    public static void OpenPrefab()
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets/Prefabs"));
    }
}
