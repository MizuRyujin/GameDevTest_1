using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset = 5f;
    private PlayerController _playerRef;
    Vector3 _newPos;
    bool resetting;

    private void Awake()
    {
        _playerRef = FindObjectOfType<PlayerController>();
        _playerRef.OnDeath += ResetCamera;
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
        if (!resetting)
        {
            transform.position = _newPos;
        }
    }

    private void MoveTowardsPlayer()
    {
        _newPos = transform.position;
        Vector3 distance = _playerRef.transform.position - transform.position;

        if (distance.z > _zOffset)
        {
            _newPos.z = Mathf.Lerp(_newPos.z, _playerRef.transform.position.z - _zOffset, 1f);
        }
        if (distance.y < -_yOffset)
        {
            _newPos.y = Mathf.Lerp(_newPos.y, _playerRef.transform.position.y + _yOffset, 1f);
        }
    }

    public void ResetCamera()
    {
        resetting = true;
        Debug.Log("Resetting Cam");
        transform.position = new Vector3(_playerRef.transform.position.x,
                            _playerRef.transform.position.y + _yOffset,
                            _playerRef.transform.position.z + _zOffset);
        resetting = false;
    }
}
