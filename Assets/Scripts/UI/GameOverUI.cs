using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    [Header("Quit Button")] [SerializeField] private Button quitButton;

    private void Awake()
    {
     Hide(); 
     quitButton.onClick.AddListener(Application.Quit);
    }

    public void ShowWin(int collected, int total, float timeSeconds)
    {
        Show();
        
        titleText.text = "Game Over";
        coinsText.text = $"Coins: {collected} / {total}";
        timeText.text = $"Time: {FormatTime(timeSeconds)}";
    }

    private string FormatTime(float timeSeconds)
    {
        int minutes      = Mathf.FloorToInt(timeSeconds / 60f);
        int seconds      = Mathf.FloorToInt(timeSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeSeconds * 1000f) % 1000f);
        
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
