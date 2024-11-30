using System.Collections;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    public enum EnumCurveChoices
    {
        EntryMainMenu,
        EntryUXUI,
        EntryThreeDArt,
        EntryProgramming,
        EntryVFX,

        IntroUXUI,
        OutroUXUI,

        IntroThreeDArt,
        OutroThreeDArt,

        IntroProgramming,
        OutroProgramming,

        IntroVFX,
        OutroVFX,
    }

    [System.Serializable]
    public struct TimeCurveCategory
    {
        public EnumCurveChoices categoryName;
        public AnimationCurve curve;
        public float duration;
    }

    public TimeCurveCategory[] timeCurves;

    private Coroutine activeCoroutine;

    public void PlayTimeCurve(EnumCurveChoices category)
    {
        foreach (var timeCurve in timeCurves)
        {
            if (timeCurve.categoryName == category)
            {
                if (activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }
                activeCoroutine = StartCoroutine(PlayCurveCoroutine(timeCurve.curve, timeCurve.duration));
                return;
            }
        }
    }

    private IEnumerator PlayCurveCoroutine(AnimationCurve curve, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float normalizedTime = elapsedTime / duration;
            float currentValue = curve.Evaluate(normalizedTime);
            Time.timeScale = currentValue;
            yield return null; // Wait for the next frame
        }

        Time.timeScale = 1.0f;
        activeCoroutine = null;
    }
}
