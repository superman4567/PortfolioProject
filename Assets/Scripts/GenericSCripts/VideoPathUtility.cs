using System.IO;
using UnityEngine;

public static class VideoPathUtility
{
    public static string GetVideoUrl(string fileName)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Application.streamingAssetsPath + "/Videos/" + fileName;
#elif UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Videos", fileName);
        return "file:///" + path;
#else
        return Path.Combine(Application.streamingAssetsPath, "Videos", fileName);
#endif
    }
}
