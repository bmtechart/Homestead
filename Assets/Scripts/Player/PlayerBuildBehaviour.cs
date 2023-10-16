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
        //building preview follows mouse cursor
        if(buildingPreview)
        {
            buildingPreview.transform.position = GameManager.GetInstance().GetCursorController().transform.position;
        }
    }

    public void OnEnterBuildMode()
    {
        buildingPreview = Instantiate(selectedBuildingPrefab);
        buildingPreview.GetComponent<BuildingController>().PreviewMode = true;
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
        //buildingPreview = newBuildingPrefa from index;
        //increment building Index
        //set selectedBuildingPrefab based on new index
        //buildingPreview = Instantiate(selectedBuildingPrefab);
    }

    public void Build()
    {
        if (!buildingPreview.GetComponent<BuildingController>().canBePlaced) return;
        GameObject newBuilding = Instantiate(selectedBuildingPrefab);
        newBuilding.transform.SetPositionAndRotation(buildingPreview.transform.position, buildingPreview.transform.rotation);
        newBuilding.GetComponent<BuildingController>().Build();
        //newBuilding.GetComponent<BuildingController>().PreviewMode=false;   
    }
}
