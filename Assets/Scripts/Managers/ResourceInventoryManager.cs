using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceInventoryManager : Singleton<ResourceInventoryManager>
{
    #region params
    [Tooltip("Amount of wood at the start of the game")]
    [SerializeField] private int startingCurrency = 100;
    private int currency;

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
        currency = startingCurrency;

        crystal = startingCrystal;
    }

    public int GetResourceCount(ResourceTypes resourceType) 
    {
        int returnValue;
        switch (resourceType)
        {
            case ResourceTypes.Currency:
                returnValue = currency;
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
            case ResourceTypes.Currency: 
                currency += amount; 
                break;
            case ResourceTypes.Crystal: 
                crystal += amount; 
                break;
            default: 
                break;
        }

        ResourceInventoryUpdated?.Invoke();
    }

    #endregion
}
