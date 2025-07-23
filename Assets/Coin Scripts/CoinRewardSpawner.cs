using UnityEngine;
using UnityEngine.UI;

public class CoinRewardSpawner : MonoBehaviour
{
    public int minCoins = 5;
    public int maxCoins = 10;
    public float spawnRadius = 0.5f;
    public AnimationCurve moveCurve;
    public float coinSpeed = 200f;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnCoins);
    }

    private void SpawnCoins()
    {
        int count = Random.Range(minCoins, maxCoins + 1);
        Vector3 target = CoinManager.Instance.coinTargetPosition;

        for (int i = 0; i < count; i++)
        {
            // Grab a coin from the pool
            GameObject coinObj = CoinPool.Instance.GetCoin();
            Coin coin = coinObj.GetComponent<Coin>();

            // Generate a fresh random offset *for each* coin
            Vector2 randomOffset2D = Random.insideUnitCircle * spawnRadius;
            Vector3 startPos = transform.position + new Vector3(randomOffset2D.x, randomOffset2D.y, 0f);

            // Immediately position the coin at its start
            coinObj.transform.position = startPos;

            // Initialize its movement toward the target
            coin.Initialize(startPos, target, coinSpeed, moveCurve);

            //destroy this script after spawning coins
            Destroy(this);
        }
    }
}
