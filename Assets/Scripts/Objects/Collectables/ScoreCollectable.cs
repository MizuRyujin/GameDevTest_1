using UnityEngine;

public class ScoreCollectable : BaseCollectable
{
    [SerializeField] private int _scoreValue = 10;

    protected override void OnCollection(Collider collector)
    {
        PlayerController score;

        if (collector.TryGetComponent<PlayerController>(out score) ||
            collector.transform.parent.TryGetComponent<PlayerController>(out score))
        {
            _collected = true;
            score.SetNewScore(_scoreValue);
        }
    }
}
