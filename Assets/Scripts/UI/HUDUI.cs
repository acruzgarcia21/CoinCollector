using System;
using TMPro;
using UnityEngine;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void SetCoins(int collected, int total) 
        => coinsText.text = $"Coins: {collected} / {total}";

    public void SetTime(float timeSeconds)
        => timeText.text = $"Time: {timeSeconds:0.00}";

}
