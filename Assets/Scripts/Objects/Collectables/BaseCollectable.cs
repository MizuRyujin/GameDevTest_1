using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    private Collider _selfCol;
    protected bool _collected;

    protected abstract void OnCollection(Collider collector);

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _selfCol = GetComponent<Collider>();
        _selfCol.isTrigger = true;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(_collected) return;
        OnCollection(other);
        Destroy(this.gameObject);
    }
}
