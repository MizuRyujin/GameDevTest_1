using System.Collections;
using UnityEngine;

public class JumpZone : MonoBehaviour
{
    [SerializeField] private int _forceImpulse = 10;
    [SerializeField, Range(0.1f, 1f)] private float _lowGravTime = 0.5f;
    [SerializeField] private int _gravityReduction = 2;
    private WaitForSeconds _timer;
    private Vector3 _defaultGrav;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _timer = new WaitForSeconds(_lowGravTime);
        _defaultGrav = Physics.gravity;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.Rb.AddForce(player.Rb.velocity + transform.up * _forceImpulse, ForceMode.Impulse);
            StartCoroutine(ReduceGravity());
        }
    }

    private IEnumerator ReduceGravity()
    {
        Physics.gravity = Physics.gravity / _gravityReduction;
        yield return _timer;
        Physics.gravity = _defaultGrav;
    }
}
