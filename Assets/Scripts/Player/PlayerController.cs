using UnityEngine;
using NaughtyAttributes;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    public event Action OnDeath;

    [SerializeField, Expandable]
    private PlayerStats _playerStats;

    [SerializeField]
    private Camera _playerCam;
    private Vector3 _moveDir;
    private Vector3 _mousePos;
    private Vector3 _lastPos;
    private float _feetRadius = 0.15f;
    private bool _isTouching;
    private Ray _ray;


    public bool IsGrounded
    {
        get
        {
            Collider[] results = new Collider[1];
            int hits = Physics.OverlapSphereNonAlloc(transform.position,
                                                        _feetRadius, results,
                                                        LayerMask.GetMask("Ground"));
            return results[0] != null;
        }
    }

    public void Die()
    {
        GameManager.Instance.RestarLevel();
        OnDeath?.Invoke();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _moveDir = Vector3.zero;
        _lastPos = Vector3.zero;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.PlayerRef = this;
        GameManager.Instance.OnPauseGame += OnPause;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (GameManager.Instance.IsPaused) return;
        // _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
        //                                                 _main.nearClipPlane + 1);
        CheckTouch();
        LockZRotation();
        UprightOnGround();
    }

    private void CheckTouch()
    {
        _mousePos = Input.mousePosition;
        _ray = _playerCam.ScreenPointToRay(_mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            _isTouching = true;

            if (Physics.Raycast(_ray, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
            {
                _lastPos = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isTouching = false;
        }
    }

    /// <summary>
    /// Straightens the player while on the ground
    /// </summary>
    private void UprightOnGround()
    {
        if (IsGrounded && transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.identity, 2f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Locks RigidBody's Z rotation while on the ground.
    /// </summary>
    private void LockZRotation()
    {
        if (IsGrounded)
        {
            Rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) return;
        MoveOnTouch();
        ContinuousMoveForward();
    }

    private void OnPause()
    {
        Debug.Log("No behaviour defined when paused");
    }

    private void ContinuousMoveForward()
    {
        _moveDir += Vector3.forward * _playerStats.Acceleration * Time.fixedDeltaTime;
        _moveDir.z = _moveDir.z > _playerStats.MaxSpeed ? _playerStats.MaxSpeed : _moveDir.z;
        _moveDir.y = Rb.velocity.y;
        Rb.velocity = _moveDir;
    }

    private void MoveOnTouch()
    {
        RaycastHit hit;

        if (_isTouching)
        {
            if (Physics.Raycast(_ray, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                Vector3 newPos = transform.position;
                Vector3 curMousePos = hit.point;

                Vector3 deltaPos = curMousePos - _lastPos;
                newPos += deltaPos;
                Rb.MovePosition(new Vector3(newPos.x, transform.position.y, transform.position.z));
                _lastPos = hit.point;
            }
        }
    }
}
