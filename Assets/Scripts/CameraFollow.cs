using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset = 5f;
    private PlayerController _playerRef;
    Vector3 newPos;

    private void Awake()
    {
        _playerRef = FindObjectOfType<PlayerController>();
    }

    // private void Start()
    // {
    //     StartCoroutine(MoveTowardsPlayer());
    // }

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
        transform.position = newPos;
    }

    private void MoveTowardsPlayer()
    {
        newPos = transform.position;
        Vector3 distance = _playerRef.transform.position - transform.position;
        Debug.Log(distance);

        if (distance.z > _zOffset)
        {
            newPos.z = Mathf.Lerp(newPos.z, _playerRef.transform.position.z - _zOffset, 1f);
        }
        if (distance.y < -_yOffset)
        {
            newPos.y = Mathf.Lerp(newPos.y, _playerRef.transform.position.y + _yOffset, 1f);
        }
    }
}
