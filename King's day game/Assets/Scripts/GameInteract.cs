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
    public int maxPucks = 3;
    //public GameObject scoreUI;
    //public TMPro.TextMeshProUGUI scoreText;

    float cameraXRotation = 0f;
    float cameraYRotation = 180f;

    bool talking = false;
    bool playing = false;
    bool playerInTrigger = false;
    private int puckCount = 0;
    private int totalScore = 0;
    private GameObject currentPuck;
    private bool isInMinigame = false;
    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking && !playing)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);
              
            }
            else if (!talking || playing)
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

        if (isInMinigame && currentPuck == null)
        {
            if (puckCount < maxPucks)
            {
                SpawnPuck();    
            }
            else if (puckCount == maxPucks)
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
        talking = false;
        speech.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        cameraMovement.target = playGame;

        puckCount = 0;
        totalScore = 0;
        isInMinigame = true;
        //scoreUI.SetActive(true);
        //scoreText.text = "Score: 0";

        SpawnPuck();
    
    }

    void SpawnPuck()
    {
        currentPuck = Instantiate(puckPrefab, puckSpawnPoint.position, puckSpawnPoint.rotation);
        currentPuck.GetComponent<Puck>().Setup(this);
    }

    public void AddScore(int amount) 
    {
        totalScore += amount;
        //scoreText.text = "Score: " + totalScore;
    }

    public void NotifyPuckGone() 
    {
        puckCount++;    
        currentPuck = null;
        Debug.Log($"Puck gone. Count: {puckCount}/{maxPucks}");
    }

    void EndMinigame()
    {
        isInMinigame = false;
        //scoreUI.SetActive(false);
        cameraMovement.target = playerCam;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
        playing = false;
        cameraMovement.enabled = true;
        Debug.Log("Game ended. Final Score: " + totalScore);
    }

}
