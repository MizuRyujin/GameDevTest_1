using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController _playerRef;
    private Animator _animator;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _playerRef = GetComponent<PlayerController>();
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _animator.SetFloat("Blend", _playerRef.Rb.velocity.z);
    }
}
