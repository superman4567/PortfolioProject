using UnityEngine;

public class TimeScaleToggler : MonoBehaviour
{
    #if UNITY_EDITOR

    private float normalTimeScale = 1f;
    private float slowedTimeScale = 0.01f;
    private bool isTimeSlowed = false;

    void Update()
    {
        // Check for Numpad 0 key press
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ToggleTimeScale();
        }
    }

    private void ToggleTimeScale()
    {
        if (isTimeSlowed)
        {
            Time.timeScale = normalTimeScale;
            isTimeSlowed = false;
        }
        else
        {
            Time.timeScale = slowedTimeScale;
            isTimeSlowed = true;
        }
    }

    #endif
}
