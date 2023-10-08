using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildBehaviour : MonoBehaviour
{
    public GameObject selectedBuildingPrefab;

    private GameObject buildingPreview;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buildingPreview)
        {
            buildingPreview.transform.position = GameManager.GetInstance().GetCursorController().transform.position;
        }
    }

    public void OnEnterBuildMode()
    {
        buildingPreview = Instantiate(selectedBuildingPrefab);
        //instantiate selectedBuildingPrefab
    }

    public void OnExitBuildMode()
    {
        Destroy(buildingPreview);
        //destroy currently selected prefab
    }

    public void OnSwapBuilding(int index)
    {
        Destroy(buildingPreview);
        //increment building Index
        //set selectedBuildingPrefab based on new index
        //buildingPreview = Instantiate(selectedBuildingPrefab);
    }

    public void Build()
    {
        buildingPreview.GetComponent<BuildingController>().Build();
        /*
        GameObject newBuilding = Instantiate(buildingPreview);
        newBuilding.transform.SetPositionAndRotation(buildingPreview.transform.position, buildingPreview.transform.rotation);
        BuildingController newBuildingController = newBuilding.GetComponent<BuildingController>();
        if (newBuildingController) newBuildingController.Build();
        */
    }
}
