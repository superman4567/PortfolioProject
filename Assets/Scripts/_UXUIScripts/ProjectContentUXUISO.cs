using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ProjectContentUXUISO", menuName = "Scriptable Objects/ProjectContentUXUISO")]
public class ProjectContentUXUISO : ScriptableObject
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
        public string videoName;
    }

    public List<MediaItem> MediaItems = new();
}
