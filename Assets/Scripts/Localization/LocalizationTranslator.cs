using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEditor.Localization;

public class LocalizationTranslator : MonoBehaviour
{
    [Header("API Settings")]
    public string apiKey = "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"; // Replace with your actual API key.
    public string targetLanguage = "nl"; // Target language for translation

    private string apiUrl = "https://api.openai.com/v1/chat/completions"; // Updated URL for GPT models

    public void TranslateText(string text, System.Action<string> callback)
    {
        StartCoroutine(TranslateCoroutine(text, callback));
    }

    private IEnumerator TranslateCoroutine(string text, System.Action<string> callback)
    {
        string prompt = $"Translate the following text to {targetLanguage}: {text}";

        var postData = new
        {
            model = "gpt-4", // Use GPT-4 or GPT-3.5 as needed
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = 200
        };

        string jsonData = JsonUtility.ToJson(postData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<OpenAIResponse>(response);

                if (responseObject.choices.Length > 0)
                {
                    string translatedText = responseObject.choices[0].message.content;
                    callback?.Invoke(translatedText);
                }
                else
                {
                    Debug.LogError("No translation returned.");
                }
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response: {request.downloadHandler.text}");
            }
        }
    }

    [System.Serializable]
    public class OpenAIResponse
    {
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }

    [System.Serializable]
    public class Message
    {
        public string content;
    }
}
