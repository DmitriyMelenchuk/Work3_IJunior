using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CoinCollection _coinCollection;
    private int _balanceCoin;

    private void Awake()
    {
        _coinCollection = GetComponent<CoinCollection>();
    }

    private void OnEnable()
    {
        _coinCollection.PlayerReached += OnCollectCoin;
    }

    private void OnDisable()
    {
        _coinCollection.PlayerReached -= OnCollectCoin;
    }

    private void OnCollectCoin()
    {
        _balanceCoin++;
    }
}
