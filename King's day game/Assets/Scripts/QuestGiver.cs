using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform playerCam;

    public GameObject howToInteract;
    public GameObject speech;
    public GameObject quest;

    public GameObject questButton;
    public GameObject giveCube;

    float cameraXRotation = 0f;
    public float cameraYRotation = 0f;

    bool talking = false;
    bool playerInTrigger = false;
    public TextMeshProUGUI task4;
    public TextMeshProUGUI task4Hint;
    Tasks tasks;
    public GameObject done;
    public float cost;
    QuestUpdate questUpdate;
    bool isTriggered;
    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
        isTriggered = false;
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = false;
                howToInteract.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);
            }
            else if (!talking)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                talking = false;
                cameraMovement.interacting = false;
            }
        }

        if(playerInTrigger && Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
            cameraMovement.target = playerCam;
            Cursor.lockState = CursorLockMode.Locked;
            speech.SetActive(false);
            talking = false;
            cameraMovement.interacting = false;
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

    public void Quest()
    {
        quest.SetActive(true);
        tasks.TaskUpdated();
        questButton.SetActive(false);
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "I'd hate to ask.. but can you do me a favour? My nieces birthday is coming up but and I have no clue what kids are into nowadays. Could you pick something out from the market for me? I'll make sure to pay you back";
        TaskManager.quest4Found = true;
    }

    public void QuestUpdate()
    {
        giveCube.SetActive(true);
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "Hey, did you manage to find something?";
        if (!isTriggered)
        {
            tasks.TaskUpdated();
            isTriggered = true;
        }
    }

    public void CubeGiven()
    {
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "Thank you so much! Here's your money back, as promised";
        moneyManager.cashAmount += cost;
        task4.text = $"<s>Find the guy a birthday present</s>";
        task4Hint.text = $"<s>Current objective: Give the present to the guy</s>";
        TaskManager.quest4Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
        giveCube.SetActive(false);
    }
}
