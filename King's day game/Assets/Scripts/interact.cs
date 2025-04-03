using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform seeWares;
    public Transform playerCam;

    bool browsing = false;
    bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            browsing = !browsing;

            if (browsing)
            {
                cameraMovement.target = seeWares;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if(browsing)
        {
            cameraMovement.transform.localRotation = Quaternion.Euler(20, 0, 0);
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
}
