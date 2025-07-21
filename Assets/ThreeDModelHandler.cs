using UnityEngine;

public class ThreeDModelHandler : MonoBehaviour
{
    [SerializeField] private GameObject boundForestModel;
    [SerializeField] private GameObject nissanModel;
    [SerializeField] private GameObject incurableModel;
    [SerializeField] private GameObject questForRedemptionModel;
    [SerializeField] private GameObject quantumSquadModdel;
    [SerializeField] private GameObject towerDefenceModdel;

    private GameObject[] threeDModels;

    private void OnEnable()
    {
        ThreeDProjectButton.OnProjectButtonClicked += ToggleCorrectModel;
        ThreeDProjectButton.OnProjectButtonHovered += ToggleCorrectModel;
    }

    private void OnDisable()
    {
        ThreeDProjectButton.OnProjectButtonClicked -= ToggleCorrectModel;
        ThreeDProjectButton.OnProjectButtonHovered -= ToggleCorrectModel;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add all models to the array
        threeDModels = new GameObject[]
        {
            boundForestModel,
            nissanModel,
            incurableModel,
            questForRedemptionModel,
            quantumSquadModdel,
            towerDefenceModdel
        };
    }

    private void ToggleCorrectModel(EnumThreeDProjects model)
    {
        // Deactivate all models
        foreach (GameObject modelObject in threeDModels)
        {
            modelObject.SetActive(false);
        }

        // Activate the selected model based on the Enum value
        switch (model)
        {
            case EnumThreeDProjects.BoundForest:
                boundForestModel.SetActive(true);
                break;
            case EnumThreeDProjects.Nissan:
                nissanModel.SetActive(true);
                break;
            case EnumThreeDProjects.Incurable:
                incurableModel.SetActive(true);
                break;
            case EnumThreeDProjects.QuestForRedemption:
                questForRedemptionModel.SetActive(true);
                break;
            case EnumThreeDProjects.QuantumSquad:
                quantumSquadModdel.SetActive(true);
                break;
            case EnumThreeDProjects.TowerDefence:
                towerDefenceModdel.SetActive(true);
                break;
        }
    }


}
