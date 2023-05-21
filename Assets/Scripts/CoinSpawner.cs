using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private Transform _arrayPoints;

    private Transform[] _points;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        InitializePoints();
        StartState(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            var spawnedCoin = Instantiate(_template, _points[i].position, Quaternion.identity);
        }

        yield return null;
    }

    private void StartState(IEnumerator coroutine)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(coroutine);
        }

        _currentCoroutine = StartCoroutine(Spawn());
    }

    private void InitializePoints()
    {
        _points = new Transform[_arrayPoints.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _arrayPoints.GetChild(i);
        }
    }
}
