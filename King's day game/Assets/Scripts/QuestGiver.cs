using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform playerCam;
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

    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    public void Quest()
    {
        quest.SetActive(true);
        tasks.TaskUpdated();
        questButton.SetActive(false);
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "Hey, can you do me a favour? I really wanted that white cube over there, but I forgot my wallet. Could you please buy it for me?";
    }

    public void QuestUpdate()
    {
        giveCube.SetActive(true);
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "Hey there, did you manage to find the cube?";
    }

    public void CubeGiven()
    {
        speech.GetComponentInChildren<TextMeshProUGUI>().text = "Thank you very much!";
        task4.text = $"<s>Get the guy a white cube</s>";
        task4Hint.text = $"<s>Current objective: Give the cube to the guy</s>";
        TaskManager.quest4Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
        giveCube.SetActive(false);
    }
}
