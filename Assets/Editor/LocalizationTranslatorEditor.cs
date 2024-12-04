using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationTranslator))]
public class LocalizationTranslatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalizationTranslator translator = (LocalizationTranslator)target;
        if (GUILayout.Button("Translate All Entries"))
        {
            //translator.TranslateText();
        }
    }
}
