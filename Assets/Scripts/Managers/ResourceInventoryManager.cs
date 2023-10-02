using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceInventoryManager : Singleton<ResourceInventoryManager>
{
    #region params
    [Tooltip("Amount of wood at the start of the game")]
    [SerializeField] private int startingWood = 100;
    private int wood;

    [Tooltip("Amount of stone at the start of the game")]
    [SerializeField] private int startingStone = 0;
    private int stone;

    [Tooltip("Amount of metal at the start of the game")]
    [SerializeField] private int startingMetal = 0;
    private int metal;

    [Tooltip("Amount of crystal at the start of the game")]
    [SerializeField] private int startingCrystal = 0;
    private int crystal;
    #endregion

    #region events
    public UnityEvent ResourceInventoryUpdated;
    #endregion

    #region Runtime Callbacks
    public override void Awake()
    {
        base.Awake();
        //initialize resource count
        InitializeResourceCounts();
    }
    #endregion

    #region methods

    private void InitializeResourceCounts()
    {
        wood = startingWood;
        stone = startingStone;
        metal = startingMetal;
        crystal = startingCrystal;
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
            default:
                returnValue = -1;
                break;
        }

        return returnValue;
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

    #endregion
}
