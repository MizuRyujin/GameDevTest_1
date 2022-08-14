using UnityEngine;

public class Saw : MonoBehaviour
{
    private Transform _model;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _model = transform.GetChild(0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _model.Rotate(transform.up, 100f * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Debug.LogWarning("Player should've died");
        }
        if (other.transform.parent.TryGetComponent<BarController>(out BarController bar))
        {
            BarChunk chunk = other.GetComponent<BarChunk>();
            //Find the closest point of contact between saw and chunk
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            bar.ChangeBarScale(chunk, closestPoint);
        }
    }
}
