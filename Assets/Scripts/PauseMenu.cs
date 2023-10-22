using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;


    private void Start()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))  // ESC Button.
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;  // stops time in the scene.
                gamePaused = true;
                Cursor.lockState = CursorLockMode.None;  // Unlocks the mouse for the player to interact with the pause menu.


                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                gamePaused = false;
                Time.timeScale = 1;  // Time within the level continues.

            }
        }
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
        Time.timeScale = 1;
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }


    public void LeaveGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
