using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddCoin(1);
        Debug.Log(GameManager.Instance.GetCoins());
        Debug.Log(GameManager.Instance.GetTime());
        Destroy(gameObject);
    }
}