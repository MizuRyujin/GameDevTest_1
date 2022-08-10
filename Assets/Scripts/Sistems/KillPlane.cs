using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Awake()
    {
        _bounds.center = transform.position;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, _bounds.extents);
    }
}
