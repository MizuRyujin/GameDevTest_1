using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerScore>(out PlayerScore score))
        {
            Debug.Log("Collided");
            score.SetNewScore(scoreValue);
            Destroy(this.gameObject);
        }
    }
}
