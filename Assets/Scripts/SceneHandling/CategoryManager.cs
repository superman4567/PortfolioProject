using System;
using UnityEngine;
using UnityEngine.Playables;

public class CategoryManager : MonoBehaviour
{
    [SerializeField] private CategoryData[] categories;
    [SerializeField] private PlayableDirector returnToMainDirector;

    public static Action<bool> OnShowBlackBars;


    [Serializable]
    public class CategoryData
    {
        public string name;
        public PlayableDirector entryTimeline;
        public CategoryPanelController panelController;
    }

    private void Start()
    {
        HideAllPanels();
    }

    public void ShowCategory(int index)
    {
        if (index < 0 || index >= categories.Length)
        {
            Debug.LogWarning($"Category index {index} is out of bounds.");
            return;
        }

        HideAllPanels();

        PlayableDirector director = categories[index].entryTimeline;
        if (director != null)
            director.Play();

        OnShowBlackBars?.Invoke(true);
    }

    public void ReturnToMainMenu()
    {
        HideAllPanels();

        if (returnToMainDirector != null)
            returnToMainDirector.Play();

        OnShowBlackBars?.Invoke(true);
    }

    private void HideAllPanels()
    {
        foreach (var cat in categories)
        {
            if (cat.panelController != null)
                cat.panelController.Hide();
        }
    }
}
