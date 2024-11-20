using Cinemachine;
using System.Collections;
using UnityEngine;

public class SceneHandler3DOutro : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent();
    public event OnSequenceCompletedEvent OnSequenceCompleted;

    [SerializeField] private CinemachineBrain cineBrain;

    public IEnumerator CameraSequence()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

        //yield return StartCoroutine(TransitionToCamera0());
        OnSequenceCompleted?.Invoke();
        yield return null;
    }

}
