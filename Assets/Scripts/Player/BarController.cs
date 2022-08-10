using System.Collections;
using System.Collections.Generic;
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

    public void ChangeBarScale(bool outwards)
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            _chunks[i].MoveEdgePosition(outwards);
        }
    }
}
