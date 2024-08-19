using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.FinishPoint = this.transform;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        GameManager.Instance.FinishPoint = this.transform;
    }
}
