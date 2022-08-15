using System.Collections;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioClip _pickupSFX;
    private Collider _selfCol;
    private Transform _model;

    protected bool _collected;
    protected abstract void OnCollection(Collider collector);

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _selfCol = GetComponent<Collider>();
        _selfCol.isTrigger = true;
        _model = transform.GetChild(0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _model.Rotate(_model.up, 15f * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        OnCollection(other);
        StartCoroutine(CO_PreDestroyDelay());
    }

    private IEnumerator CO_PreDestroyDelay()
    {
        ParticleSystem ps = Instantiate(_particles, transform.position, Quaternion.identity);
        ps.Play();
        AudioManager.Instance.PlayClip(_pickupSFX, AudioSourceType.SFX, true);
        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }
}
