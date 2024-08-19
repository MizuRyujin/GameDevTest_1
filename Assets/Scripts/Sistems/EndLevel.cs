using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private int _nextLevel;
    private bool _triggered;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<PlayerController>(out PlayerController player) &&
            !_triggered)
        {
            _nextLevel += 4; // 4 is the index for the first level ("Level 0")

            _triggered = true;
            if (_nextLevel <= 4)
            {
                LoadingManager.Instance.ReturnToMenu();
                return;
            }
            GameManager.Instance.LoadLevel(_nextLevel);
        }
    }
}
