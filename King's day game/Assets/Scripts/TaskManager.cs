using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public GameObject journal;
    bool journalOpen = false;
    private List<GameObject> previouslyActiveSpeechObjects = new List<GameObject>();

    public TextMeshProUGUI bookHint;
    public TextMeshProUGUI sjoelHint;
    public TextMeshProUGUI koekhapHint;
    public TextMeshProUGUI giveCubeHint;

    public TextMeshProUGUI funFactText;

    [TextArea] public List<string> funFacts;
    int lastIndex = -1;

    public static bool quest1Complete = false;
    public static bool quest2Complete = false;
    public static bool quest3Complete = false;
    public static bool quest4Complete = false;
    public static bool quest5Complete = false;

    public GameObject endOfGame;

    bool cubeAcquired;
    public bool inMinigame = false;

    private void Start()
    {
        endOfGame.SetActive(false);
        quest1Complete = false;
        quest2Complete = false;
        quest3Complete = false;
        quest4Complete = false;
        quest5Complete = false;
}
void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
        
            if (inMinigame == true)
            {
                return;
            }

            journalOpen = !journalOpen;

            if (journalOpen)
            {
                journal.SetActive(true);
                Time.timeScale = 0f;
                GiveRandomFact();

                GameObject[] talking = GameObject.FindGameObjectsWithTag("Speech");
                previouslyActiveSpeechObjects.Clear();

                foreach (GameObject obj in talking)
                {
                    if (obj.activeInHierarchy)
                    {
                        previouslyActiveSpeechObjects.Add(obj);
                        obj.SetActive(false);
                    }
                }
            }
            else
            {
                journal.SetActive(false);
                Time.timeScale = 1f;

                foreach (GameObject obj in previouslyActiveSpeechObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
                previouslyActiveSpeechObjects.Clear();
            }
        }

        if(quest1Complete && quest2Complete && quest3Complete && quest4Complete && quest5Complete)
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

    public void GiveRandomFact()
    {
        if (funFacts.Count == 0) return;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, funFacts.Count);
        }
        while (newIndex == lastIndex && funFacts.Count > 1);

        funFactText.text = funFacts[newIndex];

        lastIndex = newIndex;
    }

}
