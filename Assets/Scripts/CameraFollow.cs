using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset = 5f;
    private PlayerController _playerRef;
    Vector3 _newPos;
    bool resetting;

    private void Start()
    {
        _playerRef = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void Update()
    {
        MoveTowardsPlayer();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        transform.position = _newPos;
    }

    private void MoveTowardsPlayer()
    {
        _newPos = transform.position;
        Vector3 distance = _playerRef.transform.position - transform.position;

        if (distance.z > _zOffset)
        {
            _newPos.z = Mathf.Lerp(_newPos.z, _playerRef.transform.position.z - _zOffset, 1f);
        }
        else
        {
            _newPos.z = _playerRef.transform.position.z - _zOffset;
        }
        if (distance.y < -_yOffset)
        {
            _newPos.y = Mathf.Lerp(_newPos.y, _playerRef.transform.position.y + _yOffset, 1f);
        }
        else
        {
            _newPos.y = _playerRef.transform.position.y + _yOffset;
        }
    }
}
