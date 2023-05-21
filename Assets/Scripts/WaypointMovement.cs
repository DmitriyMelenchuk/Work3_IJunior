using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _speed;

    public event UnityAction ReachedPoint;

    private Coroutine _currentCoroutine;
    private Transform[] _points;
    private int _currentPoint;
    private float _treshold = 1f;

    private void Start()
    {
        InitializePoints();
        StartState(MoveWithDelay());
    }

    private void InitializePoints()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void StartState(IEnumerator coroutine)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(coroutine);
    }

    private IEnumerator MoveWithDelay() 
    {
        while (enabled)
        {
            if (IsPointReached())
            {
                ReachedPoint?.Invoke();
                
                _currentPoint = (int)Mathf.Repeat(_currentPoint + 1, _points.Length);
            }

            transform.position += (_points[_currentPoint].position - transform.position).normalized * (_speed * Time.deltaTime);

            yield return null;
        }
    }

    private bool IsPointReached()
    {
        return (_points[_currentPoint].position - transform.position).magnitude < _treshold;
    }
}
