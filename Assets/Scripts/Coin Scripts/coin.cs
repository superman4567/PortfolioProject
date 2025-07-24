using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private Sequence tweenSeq;

    public void Initialize(Vector3 startPos, Vector3 endPos, float speedModifier, AnimationCurve animCurve)
    {
        canvasGroup.alpha = 0f;
        transform.localScale = Vector3.zero;
        transform.position = startPos;

        float travelDuration = Vector3.Distance(startPos, endPos) / speedModifier;
        float growTime = travelDuration * 0.2f;  // grow over first 20% of move
        float shrinkTime = travelDuration * 0.2f;  // shrink from 20–40% of move

        tweenSeq?.Kill();
        tweenSeq = DOTween.Sequence()
            // fade/scale in
            .Append(canvasGroup.DOFade(1f, 0.5f))

            //wait for 0.2 seconds before starting the move
            .Insert(0f, transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack))

            // start the move
            .Append(DOTween.To(() => transform.position, x => transform.position = x, endPos, travelDuration)
                .SetEase(animCurve)
            )

            // schedule the “pop-out” to 1.6 at 20% into the move
            .Insert(0.5f + growTime,
                transform.DOScale(1.6f, growTime).SetEase(Ease.OutQuad)
            )
            // schedule the “settle back” to 1 at 40% into the move
            .Insert(0.5f + growTime + shrinkTime,
                transform.DOScale(1f, shrinkTime).SetEase(Ease.InQuad)
            )

            // fade/scale out at the end
            .Append(canvasGroup.DOFade(0f, 0.5f))
            .Join(transform.DOScale(0f, 0.5f).SetEase(Ease.InBack))

            .OnComplete(() =>
            {
                CoinManager.Instance.UpdateCoinCount(1);
                CoinPool.Instance.ReturnCoin(gameObject);
            });
    }

    private void OnDisable()
    {
        tweenSeq?.Kill();
    }
}
