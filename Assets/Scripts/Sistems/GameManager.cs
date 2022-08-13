using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }
    public PlayerController PlayerRef { get; set; }
    public Transform RestartPoint { get; set; }

    public event Action OnPauseGame;
    public event Action OnStartLevel;

    [SerializeField] private PlayerStats _playerStats;
    private int _currentLevel;


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
        Initialize();
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Confined;
        IsPaused = false;
        _currentLevel = 4 + _playerStats.CompletedLevels;
        // 4 is the index for Level 0
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardPause();
    }

    private void KeyboardPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            OnPauseGame?.Invoke();
        }
    }

    public void LoadLevel(int buildIndex)
    {
        _playerStats.IncrementCompletedLevels();
        LoadingManager.Instance.LoadLevel(buildIndex);
        PlayerRef.LevelLoaded(RestartPoint.position);
    }

    public void StartGame()
    {
        _currentLevel = 4 + _playerStats.CompletedLevels;
        if (_currentLevel > LoadingManager.Instance.ScenesInBuild - 1)
        {
            _playerStats.ResetLevels();
            _currentLevel = 4 + _playerStats.CompletedLevels;
        }
        LoadingManager.Instance.StartGame(_currentLevel);
    }
    public void PauseGame()
    {
        IsPaused = !IsPaused;
        Time.timeScale = Time.timeScale == 0 ? 0f : 1f;
        OnPauseGame?.Invoke();
    }
    public void RestarLevel()
    {
        _currentLevel = 4 + _playerStats.CompletedLevels;
        LoadingManager.Instance.RestartLevel(_currentLevel);
        PlayerRef.LevelLoaded(RestartPoint.position);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
