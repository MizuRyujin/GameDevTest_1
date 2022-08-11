using UnityEngine;

public class Collectables : MonoBehaviour, IBarResizer
{
    [SerializeField] private int scoreValue = 10;
    private bool _increaseBar;

    public void ResizeBar(BarController bar, bool outward)
    {
        bar.ChangeBothBarScale(outward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerScore>(out PlayerScore score))
        {
            Debug.Log("Collided");
            score.SetNewScore(scoreValue);
            Destroy(this.gameObject);
        }
        if (other.TryGetComponent<BarController>(out BarController bar))
        {
            ResizeBar(bar, _increaseBar);
        }
    }
}
