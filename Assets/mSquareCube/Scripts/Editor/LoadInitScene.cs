using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadInitScene : EditorWindow
{
    [MenuItem("Tools/LoadInitScene")]
    public static void LoadScene()
    {
        OpenScene("Init");
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
