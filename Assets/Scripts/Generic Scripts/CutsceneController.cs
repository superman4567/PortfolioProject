using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using DG.Tweening;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public UnityEvent onCutsceneEnd;
    public GameObject skipPromptUI;

    private bool hasSkipped = false;
    private bool isCutscenePlaying = false;
    private CanvasGroup skipCanvasGroup;

    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public float showDelay = 1f;

    private void Awake()
    {
        skipCanvasGroup = skipPromptUI.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        TapToSkipBroadcaster.OnTapToSkip += OnSkipPressed;
        director.played += OnTimelineStarted;
        director.stopped += OnTimelineStopped;
    }

    private void OnDisable()
    {
        TapToSkipBroadcaster.OnTapToSkip -= OnSkipPressed;
        director.played -= OnTimelineStarted;
        director.stopped -= OnTimelineStopped;
    }

    void Start()
    {
        skipCanvasGroup.alpha = 0;
        skipCanvasGroup.interactable = false;
        skipCanvasGroup.blocksRaycasts = false;
    }

    public void OnSkipPressed()
    {
        if (isCutscenePlaying && !hasSkipped)
            SkipCutscene();
    }

    void SkipCutscene()
    {
        hasSkipped = true;
        FadeOutSkipPrompt();
        director.time = director.duration;
        director.Evaluate();
        director.Stop();
    }

    void OnTimelineStarted(PlayableDirector dir)
    {
        FadeInSkipPrompt();
        hasSkipped = false;
        isCutscenePlaying = true;
    }

    void OnTimelineStopped(PlayableDirector dir)
    {
        FadeOutSkipPrompt();
        isCutscenePlaying = false;
        onCutsceneEnd.Invoke();
    }

    void FadeInSkipPrompt()
    {
        skipCanvasGroup.blocksRaycasts = true;
        skipCanvasGroup.interactable = true;

        skipCanvasGroup.DOKill();

        skipCanvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            skipCanvasGroup.DOFade(0.2f, fadeDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }

    void FadeOutSkipPrompt()
    {
        skipCanvasGroup.DOKill();
        skipCanvasGroup.blocksRaycasts = false;
        skipCanvasGroup.interactable = false;
        skipCanvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
    }
}
