using UnityEngine;

public class RailController : MonoBehaviour
{
    [SerializeField] private int _endOfRailImpulse = 10;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.Rb.AddForce(player.Rb.velocity + transform.up * _endOfRailImpulse);
        }
    }
}
