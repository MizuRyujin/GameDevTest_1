using System.Collections;
using UnityEngine;

public class BarController : MonoBehaviour
{
    private BarChunk[] _chunks;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _chunks = GetComponentsInChildren<BarChunk>();
    }

    public void ChangeBothBarScale(bool outwards)
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].MoveEdgePosition(outwards);
        }
        CheckChunksEqualScale();
    }
    public void ChangeBothBarScale(bool outwards, int timesToRepeat)
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].MoveEdgePosition(outwards);
        }
        CheckChunksEqualScale();
    }
    /// <summary>
    /// Changes the scale of one of the bar chunks.
    /// </summary>
    /// <param name="chunk">The chunk to be rescaled. </param>
    /// <param name="position">The new position for the chunk's edge transform</param>
    public void ChangeBarScale(BarChunk chunk, Vector3 position)
    {
        chunk.MoveEdgePosition(position);
        CheckChunksEqualScale();
    }

    /// <summary>
    /// Check if both chunks have the same scale and are centered with the player
    /// </summary>
    private void CheckChunksEqualScale()
    {
        Debug.Log($"Code comented on {this.name}");
        // if (_chunks.Length >= 2)
        // {
        //     if (_chunks[0].transform.localScale.y != _chunks[1].transform.localScale.y)
        //     {
        //         StartCoroutine(CO_ResizeBarsToEqualScale());
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Chunks lost. Check if initialization finds the correct chunks");
        // }
    }

    /// <summary>
    /// Coroutine that gets the scale difference between both chunks and rescales them
    /// </summary>
    private IEnumerator CO_ResizeBarsToEqualScale()
    {
        float scaleDiff = _chunks[1].transform.localScale.y - _chunks[0].transform.localScale.y;

        while (true)
        {
            if (_chunks[1].transform.localScale.y == _chunks[0].transform.localScale.y)
            {
                break;
            }
            yield return null;
        }
    }
}
