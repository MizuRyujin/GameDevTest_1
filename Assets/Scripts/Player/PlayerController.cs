using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _sideMovementFactor = 5f;
    private Camera _main;
    private Rigidbody _rb;
    private Vector3 _moveDir;
    private Vector3 _mousePos;
    private Vector3 _lastPos;
    private float _feetRadius = 0.15f;
    private bool _isPaused;

    public Rigidbody Rb => _rb;

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

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _main = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _moveDir = Vector3.zero;
        _lastPos = Vector3.zero;
        _isPaused = false;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        GameManager.Instance.PauseGame += () => _isPaused = !_isPaused;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (_isPaused) return;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (_isPaused) return;
        MoveOnTouch();
        ContinuousMoveForward();
    }

    private void ContinuousMoveForward()
    {
        _moveDir += Vector3.forward * _acceleration * Time.fixedDeltaTime;
        _moveDir.z = _moveDir.z > _maxSpeed ? _maxSpeed : _moveDir.z;
        _moveDir.y = _rb.velocity.y;
        _rb.velocity = _moveDir;
    }

    private void MoveOnTouch()
    {

        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _main.nearClipPlane + 1);

        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = _main.ScreenToWorldPoint(_mousePos);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = transform.position;
            Vector3 curMousePos = _main.ScreenToWorldPoint(_mousePos);

            Vector3 deltaPos = curMousePos - _lastPos;
            newPos += deltaPos * _sideMovementFactor;

            _rb.MovePosition(new Vector3(newPos.x, transform.position.y, transform.position.z));
            _lastPos = _main.ScreenToWorldPoint(_mousePos);
        }
    }
}
