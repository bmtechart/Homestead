using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BoxCollider))]
public class BuildingController : MonoBehaviour, IDamageable
{
    public UnityEvent m_OnBuildingConstructed;

    [Tooltip("Amount of each resource type that need to be consumed to place this building")]
    [SerializeField]
    private ConstructionCost[] constructionCosts;

    #region Properties
    [DefaultValue(false)]
    public bool PreviewMode
    {
        get
        {
            return _previewMode;
        }
        set
        {
            _previewMode = value;
            if (_previewMode)
            {
                AssignPreviewMaterial();
                Debug.Log("preview mode");
            }

            if (!_previewMode)
            {
                EnableCollision();
            }
        }
    }

    private bool _previewMode = false;

    #endregion


    public bool canBePlaced = true;

   
    public Material previewMaterial;

    private NavMeshObstacle navMeshObstacle;
    List<GameObject> overlappingObjects;
    BoxCollider foundationVolume;
    MeshCollider[] meshColliders;
    CapsuleCollider capsuleCollider;
    
    //original materials will replace preview material once built
    public Dictionary<MeshRenderer, Material[]> originalMaterial;

    #region Runtime Callbacks
    public virtual void Awake()
    {
        CacheOriginalMaterials(); //once the building is built, it needs to replaced its basic material with
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        CacheComponents();
        CacheOriginalMaterials();
        //previewMaterial = new Material(previewMaterial);
    }

    public void CacheComponents()
    {
        overlappingObjects = new List<GameObject>();
        foundationVolume = GetComponent<BoxCollider>();
        meshColliders = GetComponentsInChildren<MeshCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public virtual void Update()
    {
        if (!TowerManager.GetInstance().CanPlaceTowers)
        {
            canBePlaced = false;
            SetPreviewMaterialColor(canBePlaced);
        }
    }
    #endregion

    /// <summary>
    /// Create dictionary of all mesh renderers and their associated materials
    /// All materials will be replaced by a preview material while the building is being placed
    /// All materials will be reset to their base mode once the building is built
    /// </summary>
    private void CacheOriginalMaterials()
    {
        originalMaterial = new Dictionary<MeshRenderer, Material[]>();
  

        //populate dictionary storing all the base materials for each mesh renderer on the object
        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] mats = new Material[mr.materials.Length];
            mats = mr.materials;

            originalMaterial.Add(mr, mats);
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
            if (!TowerManager.GetInstance().CanPlaceTowers) canBePlaced = false;
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
            if (!TowerManager.GetInstance().CanPlaceTowers) canBePlaced = false;
            SetPreviewMaterialColor(canBePlaced);
        }
    }

    private void AssignPreviewMaterial()
    {
        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
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
        if (!_previewMode) return;
        Color previewColor;
        if (buildable)
        {
            previewColor = Color.green;
        }
        else
        {
            previewColor = Color.red;
        }

        //GetComponentInChildren<MeshRenderer>().sharedMaterial.color = previewColor;

        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] mats = mr.materials;
            foreach(Material m in mats)
            {
                m.color = previewColor;
            }
        }
        previewMaterial.color = previewColor;
    }
    #endregion

    #region Building Construction
    public virtual void Build()
    {
        if (!canBePlaced)
        {
            Destroy(gameObject);
            //play negative sound queue
            return;
        }
        //EnableCollision();
        PreviewMode = false;
        
        m_OnBuildingConstructed?.Invoke();
        TowerManager.GetInstance().AddTower(gameObject);
        //play particle effect
        //play whatever animations we want
    }

    public void EnableCollision()
    {

        foundationVolume.enabled = false;
        capsuleCollider.enabled = true;
    }

    private void ReassignOriginalMaterials()
    {
        foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] mats = mr.materials;
            for (int i = 0;i < mats.Length;i++)
            {
                mats[i] = originalMaterial[mr][i];
            }
            mr.materials = mats;
        }
    }

    public virtual void Damage(GameObject source, float damageAmount)
    {
        HealthBehaviour healthBehaviour = GetComponent<HealthBehaviour>();
        if (!healthBehaviour) return;
        healthBehaviour.Damage(damageAmount);
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
    #endregion
}