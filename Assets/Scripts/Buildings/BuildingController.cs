using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [Tooltip("Amount of each resource type that need to be consumed to place this building")]
    [SerializeField]
    private ConstructionCost[] constructionCosts;

    [SerializeField]
    private bool previewMode;
    private bool canBePlaced;
    // Start is called before the first frame update
    public virtual void Start()
    {
        //ensure collision is disabled
        //apply material which can change tint based on whether the building can be placed or not
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //if being placed,
        //  follow cursor
        // if (checkAvailableResource())
        //  if canBePlaced
        //      change colour to green
        //  else
        //      change colour to red
        //
    }

    protected virtual void Build()
    {
        if (!canBePlaced)
        {
            //play negative sound queue
            return;
        }

        //instantiate building at location
        //stop building from following mouse cursor
        //enable collision
        //
    }
}