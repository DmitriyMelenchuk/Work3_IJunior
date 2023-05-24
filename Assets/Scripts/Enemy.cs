using UnityEngine;

[RequireComponent(typeof(WaypointMovement), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private WaypointMovement _waypointMovement;
    private Animator _animator;
    private bool _faceRight;

    private static int _speedKey = Animator.StringToHash("speed");

    private void Awake()
    {
        _waypointMovement = GetComponent<WaypointMovement>();
        _animator = GetComponent<Animator>();
        _faceRight = false;
    }

    private void OnEnable()
    {
        _waypointMovement.ReachedPoint += OnReflection;
    }

    private void Update()
    {
        SetAnimation();
    }

    private void OnDisable()
    {
        _waypointMovement.ReachedPoint -= OnReflection;
    }

    private void SetAnimation()
    {
        _animator.SetFloat(_speedKey, Mathf.Abs(transform.position.x));
    }

    private void OnReflection()
    {
        if (transform.position.x > 0 && _faceRight != true || transform.position.x < 0 && _faceRight == true)
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = _faceRight != true;
        }
    }
}
