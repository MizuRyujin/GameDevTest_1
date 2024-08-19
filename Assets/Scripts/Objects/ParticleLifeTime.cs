using System.Collections;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour
{
    private ParticleSystem _self;

    private IEnumerator CO_TimeToLive()
    {
        yield return new WaitForSeconds(_self.main.duration + 0.1f);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        _self = GetComponent<ParticleSystem>();
        StartCoroutine(CO_TimeToLive());
    }
}
