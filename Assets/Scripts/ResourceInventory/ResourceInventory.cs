using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceInventory : Singleton<ResourceInventory>
{
    private int wood;
    private int stone;
    private int metal;
    private int crystal;

    public UnityEvent ResourceInventoryUpdated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetResourceCount(ResourceTypes resourceType) 
    {
        int returnValue;
        switch (resourceType)
        {
            case ResourceTypes.Wood:
                returnValue = wood;
                break;
            case ResourceTypes.Stone: 
                returnValue = stone; 
                break;
            case ResourceTypes.Metal: 
                returnValue = metal; 
                break;
            case ResourceTypes.Crystal: 
                returnValue = crystal; 
                 break;   
        }
    }

    //add or subtract from our resource inventory
    public void ModifyResource(ResourceTypes resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceTypes.Wood: 
                wood += amount; 
                break;
            case ResourceTypes.Stone: 
                stone += amount; 
                break;
            case ResourceTypes.Metal: 
                metal += amount; 
                break;
            case ResourceTypes.Crystal : 
                crystal += amount; 
                break;   
            default: 
                break;
        }

        ResourceInventoryUpdated?.Invoke();
    }


}
