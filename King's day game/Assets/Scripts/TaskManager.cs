using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class TaskManager : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public GameObject journal;
    bool journalOpen = false;
    public TextMeshProUGUI bookHint;
    public TextMeshProUGUI sjoelHint;
    public TextMeshProUGUI koekhapHint;
    public TextMeshProUGUI giveCubeHint;

    public static bool quest1Complete = false;
    public static bool quest2Complete = false;
    /*public static bool quest3Complete = false;*/
    public static bool quest4Complete = false;

    public GameObject endOfGame;

    bool cubeAcquired;

    private void Start()
    {
        endOfGame.SetActive(false);
        quest1Complete = false;
        quest2Complete = false;
        /*quest3Complete = false;*/
        quest4Complete = false;
}
void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            journalOpen = !journalOpen;

            if(journalOpen)
            {
                journal.SetActive(true);

            }
            else
            {
                journal.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }

        if(quest1Complete &&  quest2Complete /*&& quest3Complete*/ && quest4Complete)
        {
            endOfGame.SetActive(true);
            Time.timeScale = 0f;
            GameObject[] talking = GameObject.FindGameObjectsWithTag("Speech");                   
            foreach (GameObject obj in talking)
            {
                obj.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.None;
            cameraMovement.interacting = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
