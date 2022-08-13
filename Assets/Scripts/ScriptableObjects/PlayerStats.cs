using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public int PlayerScore { get; private set; }
    [field: SerializeField] public int CompletedLevels { get; private set; }

    public void IncrementCompletedLevels()
    {
        CompletedLevels++;
    }

    public void UpdateTotalScore(int newScore)
    {
        PlayerScore += newScore;
    }
}
