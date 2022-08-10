using UnityEngine;
using NaughtyAttributes;

public class BarChunk : MonoBehaviour
{
    [SerializeField] private Transform _playerCenter;
    [SerializeField] private Transform _edgeTransform;
    private Rigidbody _rb;
    private Vector3 _initialScale;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _initialScale = transform.localScale;
        _rb = GetComponent<Rigidbody>();
        UpdateBarScale();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
    }

    private void UpdateBarScale()
    {
        float distance = Vector3.Distance(_playerCenter.position, _edgeTransform.position);
        transform.localScale = new Vector3(_initialScale.x, distance * 0.5f, _initialScale.z);

        Vector3 midPoint = (_playerCenter.position + _edgeTransform.position) * 0.5f;
        transform.position = midPoint;
    }

    [Button]
    public void MoveEdgePosition(bool outwards = true)
    {
        if (!outwards)
        {
            _edgeTransform.position -= _edgeTransform.right * 0.5f;
        }
        else
        {
            _edgeTransform.position += _edgeTransform.right * 0.5f;
        }

        UpdateBarScale();
    }
}
