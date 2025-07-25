using UnityEngine;
using UnityEditor;

public class MissingReferenceFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing References in Scene")]
    static void FindMissingReferences()
    {
        // Search all active and inactive objects in the scene
        var sceneObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in sceneObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing Component in '{go.name}' at path: {GetGameObjectPath(go)}", go);
                }
            }
        }
    }

    static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        while (go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
            path = go.name + "/" + path;
        }
        return path;
    }
}
