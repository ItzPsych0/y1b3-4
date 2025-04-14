using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interact : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform seeWares;
    public Transform playerCam;
    public GameObject speech;

    float cameraXRotation = 0f;

    [SerializeField] bool talking = false;
    [SerializeField] bool browsing = false;
    bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking && !browsing)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);
            }
            else if(!talking || browsing)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                browsing = false;
                talking = false;
            }
        }

        if(talking)
        {
            cameraXRotation = 0;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
        }

        if(browsing)
        {
            cameraXRotation = 20;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
            speech.SetActive(false);
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

    public void SeeWares()
    {
        browsing = true;
        talking = false;
        cameraMovement.target = seeWares;
    }
}
