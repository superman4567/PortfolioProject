using System;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TextMeshProUGUI coinText;
    public RectTransform coinTarget;

    private int totalCoins;
    public Vector3 CoinTargetPosition => coinTarget.position;

    public static event Action<int> OnCoinCollected;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        totalCoins = 0;
        UpdateCoinCount(totalCoins);
    }

    public void UpdateCoinCount(int amount)
    {
        totalCoins += amount;
        coinText.text = totalCoins.ToString();
        OnCoinCollected?.Invoke(totalCoins);
    }
}
