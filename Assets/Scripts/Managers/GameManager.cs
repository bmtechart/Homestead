using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    #region Events

    public UnityEvent<bool> m_OnGameOver; //true for victory, false for defeat
    public UnityEvent m_OnGameStart;

    #endregion

    private PlayerController playerController;
    private CursorController cursorController;
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

    public void GameOver(bool playerVictory)
    {
        m_OnGameOver?.Invoke(playerVictory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
