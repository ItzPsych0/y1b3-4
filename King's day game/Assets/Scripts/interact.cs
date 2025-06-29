using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class interact : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public InteractManager interactManager;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform seeWares;
    public Transform playerCam;
    public GameObject howToInteract;
    public GameObject speech;
    public GameObject poor;

    Light currentLight = null;
    Light previousLight = null;

    float cameraXRotation = 0f;
    public float cameraYRotation = 0f;

    [TextArea] public string dialogueText;
    public bool talking = false;
    public bool browsing = false;
    bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking && !browsing)
            {
                interactManager.Dialogue(this);
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = false;
                howToInteract.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                PauseManager.isInteracting = true;
            }
            else if(!talking || browsing)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                poor.SetActive(false);
                browsing = false;
                talking = false;
                PauseManager.isInteracting = false;
                if (currentLight != null)
                {
                    currentLight.enabled = false;
                }
                cameraMovement.interacting = false;
            }
        }

        if(playerInTrigger && Input.GetKeyUp(KeyCode.Escape) && (talking || browsing))
        {
            GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
            cameraMovement.target = playerCam;
            Cursor.lockState = CursorLockMode.Locked;
            speech.SetActive(false);
            poor.SetActive(false);
            browsing = false;
            talking = false;
            PauseManager.isInteracting = false;
            if (currentLight != null)
            {
                currentLight.enabled = false;
            }
            cameraMovement.interacting = false;
        }

        if (talking)
        {
            cameraXRotation = 0;
            cameraMovement.transform.position = talkWithNPC.transform.position;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
            cameraMovement.interacting = true;
        }

        if (browsing)
        {
            Browsing();
            cameraMovement.transform.position = seeWares.transform.position;
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

    public void SeeWares()
    {
        browsing = true;
        talking = false;
        cameraMovement.target = seeWares;
    }

    void Browsing()
    {
        cameraXRotation = 20;
        cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
        speech.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = LayerMask.GetMask("Wares");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            currentLight = hit.transform.gameObject.GetComponentInChildren<Light>(true);

            if (currentLight != null && currentLight != previousLight)
            {
                if (previousLight != null)
                    previousLight.enabled = false;

                currentLight.enabled = true;
                previousLight = currentLight;
            }

            float cost = currentLight.GetComponentInParent<Value>().value;

            if (Input.GetMouseButtonDown(0))
            {
                if (moneyManager.cashAmount >= cost)
                {
                    moneyManager.cashAmount -= cost;
                    Destroy(currentLight.transform.parent.gameObject);
                    previousLight = null;
                }
                else
                {
                    poor.SetActive(true);
                    TextMeshProUGUI poorText = poor.GetComponentInChildren<TextMeshProUGUI>();
                    poorText.text = $"Sorry, you need €{cost} to buy this.";
                }
            }
        }
        else
        {
            if (previousLight != null)
            {
                previousLight.enabled = false;
                previousLight = null;
            }

            if (currentLight != null)
            {
                currentLight.enabled = false;
            }

            poor.SetActive(false);
        }
    }

}
