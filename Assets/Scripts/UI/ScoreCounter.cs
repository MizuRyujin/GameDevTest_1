
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private PlayerScore _playerScore;
    private TMP_Text _text;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _playerScore = FindObjectOfType<PlayerScore>();
        _playerScore.OnNewScore += DisplayNewScore;
        _text = GetComponent<TMP_Text>();
        _text.text = _playerScore.GetScore().ToString();
    }

    private void DisplayNewScore(int obj)
    {
        _text.text = _playerScore.GetScore().ToString();
    }
}
