using UnityEngine;

public class BarCollectable : BaseCollectable
{
    [SerializeField] protected bool _shouldIncrease;

    protected override void OnCollection(Collider collector)
    {
        BarController controller;

        if (collector.TryGetComponent<BarController>(out controller))
        {
            Debug.Log(collector.name);
            controller.ChangeBothBarScale(_shouldIncrease);
            _collected = true;
            Destroy(this.gameObject);
        }
        Debug.Log(_collected);
    }
}
