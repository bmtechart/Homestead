using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildBehaviour : MonoBehaviour
{
    public GameObject ConstructionPrefab;

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

    public void StartBuildingPlacement()
    {
        buildingPreview = Instantiate(ConstructionPrefab);
    }

    public void StopBuildingPlacement()
    {
        buildingPreview = null;
    }
}
