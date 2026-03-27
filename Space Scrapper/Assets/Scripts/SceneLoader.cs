using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        StartMenuScene,
        LoadingScene,
        MainSpaceshipScene,
    }
    
    private static Scene targetScene;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadSceneAsync(targetScene.ToString());
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            // Unity AsyncOperation.progress goes from 0 to 0.9. 
            // 1.0 is only reached when the scene activates.
            return loadingAsyncOperation.progress / 0.9f; 
        }
        return 0f;
    }
}
