using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorController : BuildingController
{
    [Tooltip("The type of resource this building generates")]
    [SerializeField] private ResourceTypes resourceType;
    [Tooltip("The quantity of resources created every time the Genearte() method is called on this controller.")]
    [SerializeField] private int amountGenerated;

    [SerializeField] private Dictionary<ResourceTypes, int> constructionCost;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Generate()
    {
        ResourceInventory.GetInstance().ModifyResource(resourceType, amountGenerated);
        Debug.Log(amountGenerated.ToString() + " " + resourceType.ToString() + " generated!");
    }
}
