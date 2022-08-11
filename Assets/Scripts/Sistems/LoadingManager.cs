using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private List<AsyncOperation> _scenesToLoad;
    public event Action<float> WhileLoading;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Starts the game. To be used from main menu, start button
    /// </summary>
    public void StartGame()
    {
        _scenesToLoad = new List<AsyncOperation>(){
            SceneManager.LoadSceneAsync(1), // Loading Screen
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive), // BaseScene
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive), // First Level
        };
        StartCoroutine(TrackLoadProgress());
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
        SceneManager.UnloadSceneAsync(1).completed += _ =>
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
    }

    public void ReturnToMenu()
    {
        _scenesToLoad.Clear();
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(1));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive));
    }
}
