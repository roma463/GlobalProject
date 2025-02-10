using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameTools : EditorWindow
{
    [MenuItem("Tools/ClearSave")]
    public static void ClearSave()
    {
        EditorPrefs.DeleteAll();
        Debug.Log("Clear all save complete");
    }

    [MenuItem("Tools/LoadingInit")]
    public static void LoadInit()
    {
        OpenScene("Init");
    }

    [MenuItem("Tools/LoadingMenu")]
    public static void LoadMenu()
    {
        OpenScene("Menu");
    }

    private static void OpenScene(string sceneName)
    {
        string[] guids = AssetDatabase.FindAssets($"t:Scene {sceneName}");

        if (guids.Length == 0)
        {
            EditorUtility.DisplayDialog("ќшибка", $"—цена с именем \"{sceneName}\" не найдена.", "OK");
            return;
        }

        string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
        EditorSceneManager.OpenScene(scenePath);
    }
}
