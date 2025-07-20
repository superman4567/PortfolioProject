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

    private void OnEnable()
    {
        ReturnButton.OnReturnTocategory += ReturnToMainMenu;
    }

    private void OnDisable()
    {
        ReturnButton.OnReturnTocategory -= ReturnToMainMenu;
    }

    private void Start()
    {
        HideAllPanels();
    }

    public void ShowCategory(int index)
    {
        HideAllPanels();

        PlayableDirector director = categories[index].entryTimeline;
        if (director != null)
            director.Play();

        OnShowBlackBars?.Invoke(true);
    }

    public void ShowCategoryUI(int index)
    {
        CategoryPanelController categoryPanelController = categories[index].panelController;
        categoryPanelController.Show();
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
