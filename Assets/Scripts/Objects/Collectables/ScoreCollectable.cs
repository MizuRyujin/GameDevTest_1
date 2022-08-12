using UnityEngine;

public class ScoreCollectable : BaseCollectable
{
    [SerializeField] private int _scoreValue = 10;

    protected override void OnCollection(Collider collector)
    {
        PlayerScore score;

        if (collector.TryGetComponent<PlayerScore>(out score) ||
            collector.transform.parent.TryGetComponent<PlayerScore>(out score))
        {
            score.SetNewScore(_scoreValue);
            _collected = true;
        }
    }
}
