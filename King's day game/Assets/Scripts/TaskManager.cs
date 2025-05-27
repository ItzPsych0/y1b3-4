using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
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

    bool cubeAcquired;
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

        if(quest1Complete &&  quest2Complete /*&& quest3Complete*/ && quest4Complete)
        {
            SceneManager.LoadScene(0);
        }
    }
}
