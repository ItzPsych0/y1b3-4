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
    public GameObject tutorial;


    public bool isPaused = false;
    public static bool isInteracting;

    public cameraMovement cameraMovement;

    void Start()
    {
        pauseUI.SetActive(false);
        confirmationUI.SetActive(false);
        isInteracting = false;

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(ShowQuitConfirmation);
        confirmQuitYesButton.onClick.AddListener(QuitGame);
        confirmQuitNoButton.onClick.AddListener(CancelQuit);

    }

    
    void Update()
    {
        if (isInteracting)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && confirmationUI.activeSelf == false)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        cameraMovement.interacting = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;

   
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
    }

    public void ResumeGame()
    {
        cameraMovement.interacting = false;
        pauseUI.SetActive(false);
        confirmationUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

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
        SceneManager.LoadScene(0);
    }

    public void HideGuide() { 
    
    tutorial.SetActive(false);

    }

}
