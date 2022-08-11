using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private bool _increaseBar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerScore>(out PlayerScore score))
        {
            score.SetNewScore(scoreValue);
            Destroy(this.gameObject);
        }
        if (other.TryGetComponent<BarController>(out BarController controller))
        {
            controller.ChangeBothBarScale();
        }
    }
}
