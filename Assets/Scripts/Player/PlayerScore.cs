using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _score = 0;

    public event Action<int> OnNewScore;

    public int GetScore() => _score;

    public void SetNewScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        OnNewScore?.Invoke(_score);
    }
}
