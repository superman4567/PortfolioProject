using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ProjectContent3DSO", menuName = "Scriptable Objects/ProjectContent3DSO")]
public class ProjectContent3DSO : ScriptableObject
{
    [System.Serializable]
    public class MediaItem
    {
        public enum MediaType
        {
            Image,
            Video
        }

        public MediaType Type;
        public Sprite Image;
        public VideoClip Video;
    }

    public List<MediaItem> MediaItems = new();

    public string modelName;
    public string modelStyle;
    public string modelDescription;

    public GameObject modelPrefab;

    public int modelFaces;
    public int modelVerts;
    public Sprite uVLayoutTexture;
}
