using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TextMeshProUGUI coinText;
    public RectTransform coinTarget;

    private int totalCoins;
    public Vector3 coinTargetPosition => coinTarget.position;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        totalCoins = 0;
    }

    public void UpdateCoinCount(int amount)
    {
        totalCoins += amount;
        coinText.text = totalCoins.ToString();
    }
}
