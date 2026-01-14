using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("UI")] 
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private HUDUI hudUI;

    private int _coinsCollected;
    private int _totalCoins;

    private float _elapsedTime;
    private bool _gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Count all the coins currently in the scene at the start
        Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
        _totalCoins = coins.Length;
        
        hudUI?.SetCoins(_coinsCollected, _totalCoins);
        hudUI?.SetTime(_elapsedTime);
        
        Debug.Log($"HUD ref = {(hudUI ? hudUI.name : "NULL")}");
        Debug.Log($"HUD coinsText = {hudUI?.name} / coinsTextNull? {hudUI == null}");

    }

    private void Update()
    {
        if (_gameOver) return;

        // The Time should be updated all the time, therefore its in Update
        _elapsedTime += Time.deltaTime;
        hudUI?.SetTime(_elapsedTime);
    }

    public void AddCoin(int amount)
    {
        if (_gameOver) return;

        _coinsCollected += amount;
        hudUI?.SetCoins(_coinsCollected, _totalCoins);

        if (_coinsCollected >= _totalCoins)
        {
            Win();
        }
    }

    public int GetCoins()
    {
        return _coinsCollected;
    }

    public float GetTime()
    {
        return _elapsedTime;
    }

    private void Win()
    {
        _gameOver = true;
        gameOverUI.ShowWin(_coinsCollected, _totalCoins, _elapsedTime);
    }
}
