using TMPro;

using UnityEngine;


public class GameInteract : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public int Score = 0;
    public Transform playerCam;
    public GameObject speech;
    public GameObject gameUI;
    public Transform playGame;
    public int maxPucks;
    public spawnPuck spawnPuck;

    float cameraXRotation = 0f;
    float cameraYRotation = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pucksText;

    bool talking = false;
    bool playing = false;
    bool playerInTrigger = false;
    public int puckCount = 0;
    private bool isInMinigame = false;

    public TextMeshProUGUI task3;
    public TextMeshProUGUI task3Hint;
    public GameObject done;
    public Tasks tasks;

    void Update()
    {
        // Check for interaction input (E key) when player is near NPC and not already playing minigame
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && isInMinigame == false)
        {
            talking = !talking;

            if (talking)
            {
                // Enter talking state: move camera, disable player movement, show dialogue
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);

            }
            else if (!talking)
            {
                // Exit talking state: restore camera, movement, hide dialogue
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
                cameraMovement.interacting = false;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                talking = false;
            }



        }
        // While talking: lock camera to NPC
        if (talking)

            {
                cameraXRotation = 0;
                cameraYRotation = 90;

                cameraMovement.transform.position = talkWithNPC.transform.position;
                cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
                cameraMovement.interacting = true;
            }
        // While playing minigame: lock camera to minigame position
        if (isInMinigame)
            {
                cameraXRotation = 0;
                cameraYRotation = 90;
                cameraMovement.transform.position = playGame.transform.position;
                cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
                cameraMovement.interacting = true;
            }

        // Handles the call for more pucks to spawn as well as to when call the end of the minigame
        if (isInMinigame)
            {
                if (!spawnPuck.puckInTrigger && puckCount < maxPucks)
                {
                    spawnPuck.SpawnPuck();
                    puckCount++;
                    UpdatePucksUI();
            }
                else if (puckCount >= maxPucks && !spawnPuck.puckInTrigger)
                {
                Rigidbody lastPuckRb = spawnPuck.GetCurrentPuckRigidbody();
                if (lastPuckRb != null && lastPuckRb.linearVelocity.magnitude < 0.1f)
                {
                    EndMinigame();
                }
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

    //When called will start the minigame logic
    public void PlayGame()
    {
        Score = 0;
       
        talking = false;
        playing = true;
        speech.SetActive(false);
        gameUI.SetActive(true);
        UpdateScoreUI();
        UpdatePucksUI();

        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
        Debug.Log("Switching camera to: " + playGame.name);
        cameraMovement.target = playGame;

        puckCount = 0;
        isInMinigame = true;
   
    }


    public void ScorePoints(int points)
    {
        Score += points;
        UpdateScoreUI();

        Debug.Log("Scored a total of " + Score + " points!");
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + Score;
    }

    public void UpdatePucksUI()
    {
        pucksText.text = "Pucks: " + (maxPucks+1 - puckCount);
    }

    public  void EndMinigame()
    {

        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
        cameraMovement.interacting = false;
        cameraMovement.target = playerCam;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Switching camera to: " + playerCam.name);
        playing = false;
        isInMinigame = false;
        gameUI.SetActive(false);
       

        //Removes pucks from the scene after the minigame ends
        GameObject[] remainingPucks = GameObject.FindGameObjectsWithTag("Puck");
        foreach (GameObject puck in remainingPucks)
        {
            Destroy(puck);
        }
        //Condition to win the minigame and gain a reward
        if (Score >= 15) 
        {
            moneyManager.cashAmount += 5;
            UpdateQuest();
        }

    }

    // Updates quest when minigame is completed and won
    void UpdateQuest()
    {
        task3.text = $"<s>Win a game of sjoelen</s>";
        task3Hint.text = $"<s>Current objective: find someone to play sjoelen</s>";
        TaskManager.quest3Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
    }



}