using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class RewardPopupTween : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup backgroundGroup;
    [SerializeField] private RectTransform background;
    [SerializeField] private CanvasGroup contentGroup;
    [SerializeField] private RectTransform content;
    [SerializeField] private CanvasGroup[] burstSprites;
    [SerializeField] private Transform[] burstTargets;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject objectToDestroy;

    private Vector3 initialContentPos;

    private void Awake()
    {
        closeButton.onClick.AddListener(ClosePopup);
        closeButton.interactable = false;

        initialContentPos = content.localPosition;

        // Ensure full-stretch background layout
        background.anchorMin = Vector2.zero;
        background.anchorMax = Vector2.one;
        background.pivot = new Vector2(0.5f, 0.5f);
        background.anchoredPosition = Vector2.zero;
        background.sizeDelta = Vector2.zero;

        // Reset burst sprites
        foreach (var cg in burstSprites)
        {
            if (cg != null)
            {
                cg.alpha = 0f;
                cg.transform.localPosition = Vector3.zero;
                cg.transform.localScale = Vector3.one;
                cg.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable() => AnimatePopup();

    private void AnimatePopup()
    {
        backgroundGroup.alpha = 0f;
        contentGroup.alpha = 0f;
        content.localPosition = initialContentPos + Vector3.up * 128f;

        DOTween.Sequence()
            .Append(backgroundGroup.DOFade(1f, 0.4f))
            .Append(contentGroup.DOFade(1f, 0.4f))
            .Join(content.DOLocalMoveY(initialContentPos.y, 0.4f))
            .AppendInterval(0.2f)
            .OnComplete(PlayBurst);
    }

    private void PlayBurst()
    {
        int count = Mathf.Min(burstSprites.Length, burstTargets.Length);

        for (int i = 0; i < count; i++)
        {
            var cg = burstSprites[i];
            var target = burstTargets[i];
            if (cg == null || target == null) continue;

            var sprite = cg.gameObject;
            sprite.SetActive(true);

            cg.alpha = 0f;
            sprite.transform.localPosition = Vector3.zero;
            sprite.transform.localScale = Vector3.one;

            Vector3 start = content.position;
            Vector3 end = target.position;
            float duration = 1.0f + i * 0.25f; // slower, more spaced out
            Vector3 controlPoint = (start + end) * 0.5f + Vector3.up * 1.5f;
            Vector3[] path = { start, controlPoint, end };

            DOTween.Sequence()
                .SetDelay(i * 0.05f) // slight stagger between bursts
                .Append(cg.DOFade(1f, 0.1f))
                .Join(sprite.transform.DOPath(path, duration, PathType.CatmullRom)
                    .SetEase(Ease.OutCubic)
                    .SetOptions(false))
                .Join(sprite.transform.DOScale(0f, duration).SetEase(Ease.InQuad));
        }

        StartCoroutine(EnableCloseButtonAfterDelay(0.6f));
    }

    private IEnumerator EnableCloseButtonAfterDelay(float delay)
    {
        contentGroup.interactable = true;
        contentGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(delay);
        closeButton.interactable = true;
    }

    private void ClosePopup()
    {
        backgroundGroup.DOFade(0f, 0.3f);
        contentGroup.DOFade(0f, 0.3f);
        StartCoroutine(DestroyAfterDelay(0.3f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }
}
