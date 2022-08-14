using UnityEngine;

public class BarCollectable : BaseCollectable
{
    [SerializeField] private bool _shouldIncrease;

    protected override void OnCollection(Collider collector)
    {
        BarController controller;

        if (collector.TryGetComponent<BarController>(out controller))
        {
            _collected = true;
            controller.ChangeBothBarScale(_shouldIncrease);
        }
    }
}
