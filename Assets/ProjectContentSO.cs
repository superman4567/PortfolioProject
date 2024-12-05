using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ProjectContentSO", menuName = "Scriptable Objects/ProjectContentSO")]
public class ProjectContentSO : ScriptableObject
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

    public List<MediaItem> MediaItems = new List<MediaItem>();
}
