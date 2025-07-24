using UnityEngine;

public class CoinMilestone : MonoBehaviour
{
    [SerializeField] private int milestoneValue = 100;
    [SerializeField] private GameObject milestoneEffect;

    private bool isMilestoneReached = false;

    private void OnEnable()
    {
        CoinManager.OnCoinCollected += CheckMilestone;
    }

    private void OnDisable()
    {
        CoinManager.OnCoinCollected -= CheckMilestone;
    }
    
    private void CheckMilestone(int totalCoins)
    {
        if (isMilestoneReached)
            return;

        if (totalCoins >= milestoneValue)
        {
            milestoneEffect.SetActive(true);
            isMilestoneReached = true;
        }
    }
}
