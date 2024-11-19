using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New3DProject", menuName = "ScriptableObjects/3DProject")]
public class ThreeDProjectsSO : ScriptableObject
{
    public enum ProjectStyleEnum
    {
        cartoony,
        stylized,
        semirealstic
    }

    [Header("Project Info")]
    public string projectName;
    public string projectDescription;
    public Sprite projectThumbnail;
    public ProjectStyleEnum ProjectStyle;

    [Space]

    public GameObject model;
    public int facesCount;
    public int vertsCount;
    public List<Sprite> textures;

    [Header("Animation Info")]
    public List<VideoClip> videos;
}