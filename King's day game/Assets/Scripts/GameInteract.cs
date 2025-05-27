using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameInteract : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform playerCam;
    public GameObject speech;
    public Transform playGame;
    public GameObject puckPrefab;
    public Transform puckSpawnPoint;
    public int maxPucks;
    public spawnPuck spawnPuck;
    //public GameObject scoreUI;
    //public TMPro.TextMeshProUGUI scoreText;

    float cameraXRotation = 0f;
    float cameraYRotation = 180f;

    bool talking = false;
    bool playing = false;
    bool playerInTrigger = false;
    public int puckCount = 0;
    private bool isInMinigame = false;
    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);

            }
            else if (!talking && playing == false)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                playing = false;
                talking = false;
            }
            
        }
        if (talking)
        {
            cameraXRotation = 0;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
        }
        if (isInMinigame)
        {
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0f);
        }

        if (isInMinigame)
        {
            if (!spawnPuck.puckInTrigger && puckCount < maxPucks)
            {
                spawnPuck.SpawnPuck();
                puckCount++;
            }
            else if (puckCount >= maxPucks && !spawnPuck.puckInTrigger)
            {
                EndMinigame();
            }
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

    public void PlayGame()
    {
        playing = true;
        speech.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
        Debug.Log("Switching camera to: " + playGame.name);
        cameraMovement.target = playGame;

        puckCount = 0;
        isInMinigame = true;
        //scoreUI.SetActive(true);
        //scoreText.text = "Score: 0";
    }

public  void EndMinigame()
    {
        isInMinigame = false;
        //scoreUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
        cameraMovement.target = playerCam;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Switching camera to: " + playerCam.name);
        playing = false;
       
    }



}