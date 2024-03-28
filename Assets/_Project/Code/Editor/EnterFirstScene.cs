using UnityEditor;
using UnityEditor.SceneManagement;


namespace Pokemonomania.Editor
{
    [InitializeOnLoad]
    public class PlaymodeFromFirstScene
    {
        static PlaymodeFromFirstScene()
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
            UnityEngine.Debug.Log(pathOfFirstScene + " was set as default play mode scene");
        }
    }
}