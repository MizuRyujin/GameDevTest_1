
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private PlayerController _player;
    private TMP_Text _text;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _player.OnNewScore += DisplayNewScore;
        _text = GetComponent<TMP_Text>();
        _text.text = _player.GetScore().ToString();
    }

    private void DisplayNewScore(int obj)
    {
        _text.text = _player.GetScore().ToString();
    }
}
