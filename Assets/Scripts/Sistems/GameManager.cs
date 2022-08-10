using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action PauseGame;
    public event Action StartLevel;


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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame?.Invoke();
        }
    }
}
