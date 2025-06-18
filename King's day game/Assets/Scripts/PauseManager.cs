using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject confirmationUI;
    public Button resumeButton;
    public Button quitButton;
    public Button confirmQuitYesButton;
    public Button confirmQuitNoButton;

    public bool isPaused = false;

    public cameraMovement cameraMovement;

    void Start()
    {
        pauseUI.SetActive(false);
        confirmationUI.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(ShowQuitConfirmation);
        confirmQuitYesButton.onClick.AddListener(QuitGame);
        confirmQuitNoButton.onClick.AddListener(CancelQuit);

    }


    void Update()
    {   // Checks for escape key to be pressed when not in the confirmation UI 
        if (Input.GetKeyDown(KeyCode.Escape) && confirmationUI.activeSelf == false)
        {    // used for blocking pause while talking or playing minigame
            if (!cameraMovement.interacting)
            {

                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
            else
            {
                return;
            }
        }
    }

    public void PauseGame()
    {
        cameraMovement.interacting = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;    //freeze game time
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; //unlocks cursor
    

        //Stops player movement
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
    }

    public void ResumeGame()
    {
        cameraMovement.interacting = false;
        pauseUI.SetActive(false);
        confirmationUI.SetActive(false);
        Time.timeScale = 1f;    //Resumes game time
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
      
        //Returns movement control to the player
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
    }

    public void ShowQuitConfirmation()
    {
        pauseUI.SetActive(false);
        confirmationUI.SetActive(true);
    }

    public void CancelQuit()
    {
        confirmationUI.SetActive(false);
        pauseUI.SetActive(true);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");
        SceneManager.LoadScene(0);  //Loads back to the main menu
    }
}
