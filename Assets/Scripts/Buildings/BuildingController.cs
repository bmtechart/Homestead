using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] //box collider used as trigger to see if the building can be placed
public class BuildingController : MonoBehaviour
{
    [Tooltip("Amount of each resource type that need to be consumed to place this building")]
    [SerializeField]
    private ConstructionCost[] constructionCosts;

    #region Properties
    public bool PreviewMode
    {
        get { return _previewMode; }
        set 
        { 
            _previewMode = value;
        }
    }

    private bool _previewMode = true;

    #endregion


    private bool canBePlaced = true;

   
    public Material previewMaterial;

    List<GameObject> overlappingObjects;
    BoxCollider foundationVolume;
    MeshCollider[] meshColliders;
    
    //original materials will replace preview material once built
    Dictionary<MeshRenderer, Material[]> originalMaterial;

    #region Runtime Callbacks
    // Start is called before the first frame update
    public virtual void Start()
    {

        originalMaterial = new Dictionary<MeshRenderer, Material[]>();
        overlappingObjects = new List<GameObject>();    
        foundationVolume = GetComponent<BoxCollider>();
        meshColliders = GetComponentsInChildren<MeshCollider>();
        CacheOriginalMaterials(); //once the building is built, it needs to replaced its basic material with
        AssignPreviewMaterial();
        Material newPreviewMaterial = new Material(previewMaterial); ;

        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            for(int i = 0; i < mr.materials.Length; i++)
            {
                mr.materials[i] = newPreviewMaterial;
            }
        }

        PreviewMode = true;
        
    }

    public virtual void Update()
    {

    }
    #endregion

    /// <summary>
    /// Create dictionary of all mesh renderers and their associated materials
    /// All materials will be replaced by a preview material while the building is being placed
    /// All materials will be reset to their base mode once the building is built
    /// </summary>
    private void CacheOriginalMaterials()
    {
        //create array of all mesh renderers in prefab
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //populate dictionary storing all the base materials for each mesh renderer on the object
        foreach(MeshRenderer mr in meshRenderers)
        {
            originalMaterial.Add(mr, mr.materials);
            Debug.Log(mr.materials.ToString());
        }
    }

    #region Building Placement/Preview Mode
    /// <summary>
    /// Trigger volume is used to determine whether building being constructed is overlapping another collider
    /// Building cannot be built if overlapping another collider
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "terrain") return;

        if (!overlappingObjects.Contains(other.gameObject))
        {
            overlappingObjects.Add(other.gameObject);
        }

        if(overlappingObjects.Count > 0)
        {
            canBePlaced = false;
            SetPreviewMaterialColor(canBePlaced);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "terrain") return;
        
        if(overlappingObjects.Contains(other.gameObject)) 
        { 
            overlappingObjects.Remove(other.gameObject); 
        }

        if(overlappingObjects.Count <= 0)
        {
            canBePlaced = true;
            SetPreviewMaterialColor(canBePlaced);
        }
    }

    private void AssignPreviewMaterial()
    {
        foreach(MeshRenderer mr in originalMaterial.Keys)
        {
            Material[] mats = mr.materials;
            for(int i=0; i<mats.Length; i++)
            {
                mats[i] = previewMaterial;
            }
            mr.materials = mats;
        }
    }

    /// <summary>
    /// Changes the color of the preview material based on whether the building can be placed,
    /// </summary>
    private void SetPreviewMaterialColor(bool buildable)
    {
        Color previewColor;
        if (buildable)
        {
            previewColor = Color.green;
        }
        else
        {
            previewColor = Color.red;
        }

        foreach (MeshRenderer mr in originalMaterial.Keys)
        {
            
            for (int i = 0; i < mr.materials.Length; i++)
            {
                mr.materials[i].color = previewColor;
            }
        }
    }
    #endregion

    #region Building Construction
    public virtual void Build()
    {
        if (!canBePlaced)
        {
            //play negative sound queue
            return;
        }

        GameObject newBuilding = Instantiate(gameObject);
        newBuilding.transform.SetLocalPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        BuildingController newBuildingController = newBuilding.GetComponent<BuildingController>();
        if(newBuildingController)
        {
            newBuildingController.PreviewMode = false;
            newBuildingController.EnableCollision();
            newBuildingController.ReassignOriginalMaterials();
        }
    }

    public void EnableCollision()
    {
        foreach (MeshCollider mc in GetComponentsInChildren<MeshCollider>())
        {
            mc.enabled = true;
        }
    }

    private void ReassignOriginalMaterials()
    {
        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] mats = originalMaterial[mr];
            mr.materials = mats;
        }
    }
    #endregion
}