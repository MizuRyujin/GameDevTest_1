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

    public void ResetScale()
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].ResetScale();
        }
    }

    /// <summary>
    /// Change both bar chunk local scales in a fixed increment. Overload versions
    /// can have influence in direction and increment value.
    /// </summary>
    public void ChangeBothBarScale()
    {
        StopAllCoroutines();

        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].MoveEdgePosition();
        }
        CheckChunksEqualScale();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="outwards">If the increment should be outwards or not.</param>
    public void ChangeBothBarScale(bool outwards)
    {
        StopAllCoroutines();

        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].MoveEdgePosition(outwards);
        }
        CheckChunksEqualScale();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="outwards">If the increment should be outwards or not.</param>
    /// <param name="timesToRepeat">How many times should it repeat the rescaling</param>
    public void ChangeBothBarScale(bool outwards, int timesToRepeat)
    {
        StopAllCoroutines();

        for (int i = 0; i < timesToRepeat; i++)
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
        StopAllCoroutines();

        chunk.MoveEdgePosition(position);
        CheckChunksEqualScale();
    }

    /// <summary>
    /// Check if both chunks have the same scale and are centered with the player
    /// </summary>
    private void CheckChunksEqualScale()
    {
        if (_chunks.Length >= 2)
        {
            if (_chunks[0].transform.localScale.y != _chunks[1].transform.localScale.y)
            {
                StartCoroutine(CO_ResizeBarsToEqualScale());
            }
        }
        else
        {
            Debug.LogError("Chunks lost. Check if initialization finds the correct chunks");
        }
    }

    /// <summary>
    /// Coroutine that gets the scale difference between both chunks and rescales them
    /// </summary>
    private IEnumerator CO_ResizeBarsToEqualScale()
    {
        BarChunk largest;
        BarChunk smallest;

        // Get the difference in scale
        float scaleDiff = System.Math.Abs(_chunks[1].transform.localScale.y - _chunks[0].transform.localScale.y);

        // Check which side is the largest
        if (_chunks[1].transform.localScale.y > _chunks[0].transform.localScale.y)
        {
            largest = _chunks[1];
            smallest = _chunks[0];
        }
        else
        {
            largest = _chunks[0];
            smallest = _chunks[1];
        }

        // While scales are different, rescale each appropriately
        Vector3 newEdgePos;
        while (true)
        {
            if (smallest.IsLeftSide)
            {
                newEdgePos = smallest.EdgeTransform.position + -largest.EdgeTransform.right * Time.deltaTime;

            }
            else
            {
                newEdgePos = smallest.EdgeTransform.position + smallest.EdgeTransform.right * Time.deltaTime;
            }
            smallest.MoveEdgePosition(newEdgePos);

            if (largest.IsLeftSide)
            {
                newEdgePos = largest.EdgeTransform.position + smallest.EdgeTransform.right * Time.deltaTime;
            }
            else
            {
                newEdgePos = largest.EdgeTransform.position + -largest.EdgeTransform.right * Time.deltaTime;
            }
            largest.MoveEdgePosition(newEdgePos);

            scaleDiff = System.Math.Abs(smallest.transform.localScale.y - largest.transform.localScale.y);
            if (scaleDiff <= 0.05f)
                break;

            yield return null;
        }
    }
}
