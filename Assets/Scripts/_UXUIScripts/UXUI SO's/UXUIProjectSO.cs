using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewImageData", menuName = "ScriptableObjects/ImageData")]
public class UXUIProjectSO : ScriptableObject
{
    [Header("Project Info")]
    public string projectName;
    public string projectDescription;
    public Image thumbnailImage;
    public List<UXUIProjectContent> uXUIProjectContent;
}

[Serializable]
public class UXUIProjectContent
{
    [Header("Single Image Info")]
    public string imageName;
    public string imageDescription;
    public Sprite image;
}