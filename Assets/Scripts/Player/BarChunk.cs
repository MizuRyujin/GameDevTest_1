using UnityEngine;
using NaughtyAttributes;

public class BarChunk : MonoBehaviour
{
    [SerializeField] private Transform _playerCenter;
    [SerializeField] private Transform _edgeTransform;
    [field: SerializeField] public bool IsLeftSide { get; private set; }
    private Rigidbody _rb;
    private Vector3 _initialScale;
    private Vector3 _edgeInitialPos;

    public Transform EdgeTransform => _edgeTransform;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _initialScale = transform.localScale;
        _edgeInitialPos = _edgeTransform.localPosition;
        _rb = GetComponent<Rigidbody>();
        UpdateBarScale();
    }

    private void UpdateBarScale()
    {
        float distance = Vector3.Distance(_playerCenter.position, _edgeTransform.position);
        transform.localScale = new Vector3(_initialScale.x, distance * 0.5f, _initialScale.z);

        Vector3 midPoint = (_playerCenter.position + _edgeTransform.position) * 0.5f;
        transform.position = midPoint;
    }

    public void ResetScale()
    {
        _edgeTransform.localPosition = _edgeInitialPos;
        UpdateBarScale();
    }

    [Button]
    /// <summary>
    /// Move the edge transform to a specified position.
    /// </summary>
    /// <param name="outwards">If the rescale should be outwards or not. Default true</param>
    /// <param name="increment">How much will the edge move. Default 0.5f</param>
    public void MoveEdgePosition(bool outwards = true, float increment = 0.5f)
    {
        if (!outwards)
        {
            _edgeTransform.position -= _edgeTransform.right * increment;
        }
        else
        {
            _edgeTransform.position += _edgeTransform.right * increment;
        }

        UpdateBarScale();
    }

    /// <summary>
    /// Move the edge transform to a specified position.
    /// </summary>
    /// <param name="newPosition">New edge position.</param>
    public void MoveEdgePosition(Vector3 newPosition)
    {
        _edgeTransform.position = newPosition;
        UpdateBarScale();
    }
}
