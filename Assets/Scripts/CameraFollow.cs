using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset = 5f;
    [SerializeField, Range(0.1f, 1f)] private float _lerpSpeed = 0.2f;
    private PlayerController _playerRef;

    private void Awake()
    {
        _playerRef = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        StartCoroutine(MoveTowardsPlayer());
    }

    private IEnumerator MoveTowardsPlayer()
    {
        while (true)
        {
            Vector3 newPos = transform.position;
            Vector3 distance = _playerRef.transform.position - transform.position;
            Debug.Log(distance);

            if (distance.z > _zOffset)
            {
                newPos.z += _playerRef.transform.position.z - _zOffset;
            }
            if (distance.y > _yOffset)
            {
                newPos.y += _playerRef.transform.position.y - _yOffset;
            }

            transform.Translate(newPos * Time.deltaTime);
            yield return null;
        }
    }
}
