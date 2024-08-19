using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image _loadingBar;
    private LoadingManager _manager;

    private void IncreaseLoadingBar(float percentage)
    {
        _loadingBar.fillAmount = percentage;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        _manager = FindObjectOfType<LoadingManager>();
        _loadingBar = GetComponent<Image>();
        if (_manager)
        {
            _manager.WhileLoading += IncreaseLoadingBar;
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        _manager.WhileLoading -= IncreaseLoadingBar;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        _manager.WhileLoading -= IncreaseLoadingBar;
    }
}
