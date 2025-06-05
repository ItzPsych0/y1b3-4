using TMPro;
using UnityEngine;

public class interactMinigame : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    float cameraXRotation = 0f;
    public float cameraYRotation = 0f;

    public Transform talkWithNPC;
    public Transform playerCam;

    public GameObject howToInteract;
    public GameObject speech;
    public GameObject minigame;
    bool playingGame;

    public TextMeshProUGUI task2;
    public TextMeshProUGUI task2Hint;
    public GameObject done;
    public Tasks tasks;

    bool talking = false;
    bool playerInTrigger = false;

    private void Update()
    {
        if(playingGame)
        {
            return;
        }

        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);
                howToInteract.SetActive(false);
            }
            else if (!talking)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
                cameraMovement.interacting = false;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                talking = false;
            }
        }

        if(playerInTrigger && Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
            cameraMovement.interacting = false;
            cameraMovement.target = playerCam;
            Cursor.lockState = CursorLockMode.Locked;
            speech.SetActive(false);
            talking = false;
        }

        if (talking)
        {
            cameraXRotation = 0;
            cameraMovement.transform.position = talkWithNPC.transform.position;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
            cameraMovement.interacting = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
            howToInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
            howToInteract.SetActive(false);
        }
    }

    public void PlayGame()
    {
        mainCamera.enabled = false;
        speech.SetActive(false);
        minigame.SetActive(true);
        playingGame = true;
    }
    public void Return()
    {
        playingGame = false;
        mainCamera.enabled = true;
        minigame.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
        cameraMovement.target = playerCam;
        Cursor.lockState = CursorLockMode.Locked;
        talking = false;
        cameraMovement.interacting = false;
    }

    public void VictoryReturn()
    {
        playingGame = false;
        mainCamera.enabled = true;
        minigame.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
        cameraMovement.target = playerCam;
        Cursor.lockState = CursorLockMode.Locked;
        talking = false;
        cameraMovement.interacting = false;
        UpdateQuest();

    }

    void UpdateQuest()
    {
        task2.text = $"<s>Win a game of koekhappen</s>";
        task2Hint.text = $"<s>Current objective: find someone to play koekhappen</s>";
        TaskManager.quest2Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
    }

}
