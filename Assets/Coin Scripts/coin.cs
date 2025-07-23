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

        tweenSeq?.Kill();

        tweenSeq = DOTween.Sequence()
            .Append(canvasGroup.DOFade(1f, 0.5f))
            .Join(transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
            .Append(DOTween.To(
                () => transform.position,
                x => transform.position = x,
                endPos,
                travelDuration
            ).SetEase(animCurve))
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
