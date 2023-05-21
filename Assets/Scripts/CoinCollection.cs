using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollection : MonoBehaviour
{
    public event UnityAction PlayerReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            PlayerReached?.Invoke();

            Destroy(coin.gameObject);
        }
    }
}
