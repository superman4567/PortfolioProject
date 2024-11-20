using Cinemachine;
using System.Collections;
using UnityEngine;

public class SceneHandlerProgrammingOutro : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cineBrain;

    public IEnumerator CameraSequence()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

        //yield return StartCoroutine(TransitionToCamera0());
        yield return null;
    }
}
