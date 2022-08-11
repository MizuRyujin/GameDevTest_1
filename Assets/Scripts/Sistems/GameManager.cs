using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action PauseGame;
    public event Action StartLevel;

    public bool IsPaused { get; private set; }


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
    }

    private void KeyboardPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            PauseGame?.Invoke();
        }
    }

    public void PauseButton()
    {
        IsPaused = !IsPaused;
        PauseGame?.Invoke();
    }

    public void ReturnToMenuButton()
    {
        FindObjectOfType<LoadingManager>().ReturnToMenu();
    }
}
