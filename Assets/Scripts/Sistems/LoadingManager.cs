using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    private List<AsyncOperation> _scenesToLoad;
    public event Action<float> WhileLoading;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        _scenesToLoad = new List<AsyncOperation>();
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(1));
    }

    /// <summary>
    /// Starts the game. To be used from main menu, start button
    /// </summary>
    public void StartGame()
    {
        _scenesToLoad.Clear(); // Clear any previous operations

        _scenesToLoad.Add(SceneManager.LoadSceneAsync(2)); // Loading Screen
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(
                                    3, LoadSceneMode.Additive)); // BaseScene
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(
                                    4, LoadSceneMode.Additive)); // First Level
        StartCoroutine(TrackLoadProgress());
    }

    public void RestartLevel(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    private IEnumerator TrackLoadProgress()
    {
        float progress = 0f;
        for (int i = 0; i < _scenesToLoad.Count; i++)
        {
            while (!_scenesToLoad[i].isDone)
            {
                progress += (0.01f + _scenesToLoad[i].progress) / _scenesToLoad.Count;
                WhileLoading?.Invoke(progress);
                yield return null;
            }
        }
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) // If gameplay scene is loaded
        {
            SceneManager.UnloadSceneAsync(2).completed += _ =>
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        }
        else
        {
            SceneManager.UnloadSceneAsync(2).completed += _ =>
                Resources.UnloadUnusedAssets();
        }

        Time.timeScale = Time.timeScale < 1f ? 1f : 1f;
    }

    public void ReturnToMenu()
    {
        _scenesToLoad.Clear();

        _scenesToLoad.Add(SceneManager.LoadSceneAsync(2));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));
        StartCoroutine(TrackLoadProgress());
    }
}
