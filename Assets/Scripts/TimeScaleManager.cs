using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public void SlowDownTime(float value)
    {
        Time.timeScale = value;
    }

    public void NormalTime()
    {
        Time.timeScale = 1f;
    }
}
