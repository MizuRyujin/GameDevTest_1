using UnityEngine;

public class Saw : MonoBehaviour
{
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<BarController>(out BarController bar))
        {
            BarChunk chunk = other.GetComponent<BarChunk>();
            //Find the closest point of contact between saw and chunk
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            bar.ChangeBarScale(chunk, closestPoint);
        }
    }
}
