using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseButtonBinder : MonoBehaviour
{
    public UnityAction ButtonAction;
    private Button _button;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    /// <summary>
    /// Adds methods to ButtonAction UnityAction of this button
    /// </summary>
    protected abstract void BindMethods();

    private void OnEnable()
    {
        BindMethods();
        _button.onClick.AddListener(ButtonAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
