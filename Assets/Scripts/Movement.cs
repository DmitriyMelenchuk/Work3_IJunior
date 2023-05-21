using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _targetVelocity;
    private bool _isGrounded;
    private bool _faceRight;

    private static readonly int _isGroundKey = Animator.StringToHash("isGround");
    private static readonly int _speedKey = Animator.StringToHash("speed");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _faceRight = true;
    }

    private void Update()
    {
        MoveHorizontal();
        MoveVertical();
        CheckOnGround();
    }

    private void MoveHorizontal()
    {
        _targetVelocity.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_speedKey, Mathf.Abs(_targetVelocity.x));
        _rigidbody2D.velocity = new Vector2(_targetVelocity.x * _speed, _rigidbody2D.velocity.y);

        Reflect();
    }

    private void Reflect()
    {
        if (_targetVelocity.x > 0 && _faceRight != true || _targetVelocity.x < 0 && _faceRight == true)
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = _faceRight != true;
        }
    }

    private void MoveVertical()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void CheckOnGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _layerMask);
        _animator.SetBool(_isGroundKey, _isGrounded);
    }
}
