using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }
    public PlayerController PlayerRef { get; set; }
    public event Action OnPauseGame;
    public event Action OnStartLevel;
    private Transform _restartPoint;


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
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardPause();
        CheckForRestartPoint();
    }

    private void CheckForRestartPoint()
    {
        if (_restartPoint == null)
        {
            var obj = FindObjectOfType<RestartPoint>();
            if (obj)
            {
                _restartPoint = obj.transform;
            }
        }
    }

    private void CheckForPlayerRef()
    {
        if (PlayerRef == null)
        {
            var obj = FindObjectOfType<PlayerController>();
            if (obj)
            {
                PlayerRef = obj;
            }
        }
    }

    private void KeyboardPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            OnPauseGame?.Invoke();
        }
    }

    public void RestarLevel()
    {
        LoadingManager.Instance.RestartLevel(4);
        PlayerRef.Rb.MovePosition(_restartPoint.position);
        PlayerRef.GetComponentInChildren<BarController>().ResetScale();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        IsPaused = !IsPaused;
        OnPauseGame?.Invoke();
    }
}
