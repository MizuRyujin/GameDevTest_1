using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private bool _increaseBar;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScore score;
        BarController controller;

        if (_increaseBar)
        {
            if ((other.TryGetComponent<BarController>(out controller) || other.transform.parent.TryGetComponent<BarController>(out controller)))
            {
                controller.ChangeBothBarScale();
            }
        }
        if (other.TryGetComponent<PlayerScore>(out score))
        {
            score.SetNewScore(scoreValue);
            Destroy(this.gameObject);
        }
        else if (other.transform.parent.TryGetComponent<PlayerScore>(out score))
        {
            score.SetNewScore(scoreValue);
            Destroy(this.gameObject);
        }
    }
}
