using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    #region Events

    public UnityEvent m_OnGameOver;
    public UnityEvent m_OnGameStart;

    #endregion

    private PlayerController playerController;
    private CursorController cursorController;
    private BuildingController buildingController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public CursorController GetCursorController()
    {
        if(!cursorController)
        {
            cursorController = FindObjectOfType<CursorController>();
        }

        return cursorController;
    }

    public PlayerController GetPlayerController()
    {
        if(!playerController) 
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        return playerController;
    }

    public BuildingController GetBuildingController()
    {
        if (!buildingController)
        {
            buildingController = FindObjectOfType<BuildingController>();
        }

        return buildingController;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
